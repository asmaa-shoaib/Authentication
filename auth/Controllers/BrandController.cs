using AutoMapper;
using BusinessObjects.Dto;
using BusinessObjects.Entities;
using BusinessObjects.Interfaces;
using Data_Access_Layer;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : Controller
    {
        private readonly IBrand _brand;
        private readonly Data_Base _data_Base;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _environment;
        public BrandController(IBrand brand, IMapper mapper, Data_Base data_Base, IWebHostEnvironment environment)
        {
            _data_Base = data_Base;
            _brand = brand;
            _environment = environment;
            _mapper = mapper;

        }
        [HttpPost("saveImage")]
        [Authorize(Roles = "Admin")]
        public Tuple<int, string> saveImage()
        {
            try
            {
                IFormFile imageFile = Request.Form.Files[0];
                var contentPath = this._environment.ContentRootPath;
                var path = Path.Combine(contentPath, "Uploads/Brands");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtentions = new string[] { ".jpg", ".png", ".jpeg" ,".svg"};
                if (!allowedExtentions.Contains(ext))
                {
                    string msg = string.Format("only {0} extentions are allowed", string.Join(",", allowedExtentions));
                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);

            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "error has occured");
            }
        }
        [HttpPost("createBrand")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Admin")]
        public IActionResult createBrand(BrandDto brand)
        {
            if (brand == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                //var fileResult = saveImage();
                //if (fileResult.Item1 == 1)
                //{
                //    brand.ImageUrl = fileResult.Item2;
                //}

                var brandMap = _mapper.Map<Brand>(brand);
            _brand.AddBrand(brandMap);


                //if (!_photo.Add(PhotoMap))
                //{
                //    ModelState.AddModelError("", "Something went wrong while savin");
                //    return StatusCode(500, ModelState);
                //}
            
            return Ok("Successfully created");
        }
        [HttpGet("{id}/cars")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCarInBrand(int? id)
        {
            var CarInBrand = _brand.GetCarInBrand(id);

            return Ok(CarInBrand);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Brand>))]
        public IActionResult GetBrand()
        {
            var brands = _brand.GetAll();

            return Ok(brands);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Brand))]
        [ProducesResponseType(400)]
        public IActionResult GetBrand(int? id)
        {
            if (id == null) return BadRequest("Id is required");
            Brand brand = _brand.Get(id);
            if (brand == null) return NotFound();

            return Ok(brand);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBrand(int id, Brand brand)
        {
            if (!_brand.BrandExist(id)) return NotFound();
            if (id != brand.ID) return BadRequest("id is not the old ");
            _brand.UpdateBrand(brand);
            //_data_Base.Entry(brand).State = EntityState.Modified;
            return NoContent();
            //  
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBrand(int? id)
        {
            if (id == null) return BadRequest("Id is required");
            if (!_brand.BrandExist(id)) return NotFound();
            _brand.DeleteBrand(_brand.Get(id));
            return Ok("deleted successfully");
        }
       
    }
}
