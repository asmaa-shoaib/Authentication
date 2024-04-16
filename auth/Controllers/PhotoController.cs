using AutoMapper;
using BusinessObjects.Dto;
using BusinessObjects.Entities;
using BusinessObjects.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {  //private readonly IGenericRepository<Car> _carRepository;
        private readonly ICar _car;
        private readonly IPhoto _photo;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _environment;
        // private readonly Data_Base _data_Base;
        public PhotoController(ICar car, IMapper mapper, IPhoto photo, IWebHostEnvironment environment)
        {
            _car = car;
            _photo = photo;
            _mapper = mapper;
            _environment = environment;
        }
        [HttpPost("saveImage")]
        [Authorize(Roles = "Admin")]
        public Tuple<int, string> saveImage()
        {
            try
            {
                IFormFile imageFile = Request.Form.Files[0];
                var contentPath = this._environment.ContentRootPath;
                var path = Path.Combine(contentPath, "Uploads/Photoes");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtentions = new string[] { ".jpg", ".png", ".jpeg", ".svg" };
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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Photo>))]

        public IActionResult GetPhotos()
        {
            var Photos = _photo.GetAll();
            return Ok(Photos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Photo))]
        [ProducesResponseType(400)]
        public IActionResult GetPhoto(int? id)
        {
            if (id == null) return BadRequest("id is required");
            if (!_photo.ExistPhoto(id)) return NotFound();

            var Photo = _photo.Get(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Photo);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Admin")]
        public IActionResult PostPhoto( PhotoDto Photo)
        {
            if (Photo == null)
            {
                return BadRequest(ModelState);
            }
            //if (!ModelState.IsValid)
            //{

            //    _photo.DeleteImage(Photo.Url);
            //    return BadRequest(ModelState);
            //}
             var PhotoMap = _mapper.Map<Photo>(Photo);
            _photo.Add(PhotoMap);
           //if (!_photo.Add(Photo))
           // {
           //     _photo.DeleteImage(Photo.Url);
           // }
            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult PutPhoto([FromQuery] int? id, [FromQuery] int CarId, [FromBody] PhotoDto EditedPhoto)
        {
            if (id == null) return BadRequest("id is required");
            if (id != EditedPhoto.Id) return BadRequest("id is not match");
            if (!_photo.ExistPhoto(id)) return NotFound(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var PhotoMap = _mapper.Map<Photo>(EditedPhoto);
            Car car = _car.Get(CarId);
            if (car == null) return NotFound();
            PhotoMap.Car = car;
            if (!_photo.UpdatePhoto(PhotoMap))
            {
                ModelState.AddModelError("", "error while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeletePhoto(int? id)
        {
            if (id == null) return BadRequest("id is requird");
            if (!_photo.ExistPhoto(id)) return NotFound();
            _photo.Delete(_photo.Get(id));
            return Ok();
        }
        [HttpGet("{carId}/CarDetails")]
        public IActionResult GetCarPhotos(int? carId)
        {
            if (carId == null) return BadRequest("id is requird");
            if (!_car.ExistCar(carId)) return NotFound("car is no exist");
            IEnumerable<Photo> Photos = _photo.GetCarPhotos(carId);
            if (Photos == null) return NotFound();
            return Ok(Photos);

        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           