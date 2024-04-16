using BusinessObjects.Entities;
using BusinessObjects.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BusinessObjects.Entities;

using BusinessObjects.Dto;
using Microsoft.AspNetCore.Authorization;

namespace auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICar _car;
        private readonly IBrand _brand;
        private readonly IMapper _mapper;
        public CarController(ICar car, IMapper mapper, IBrand brand)
        {
            _car = car;
            _brand = brand;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCars()
        {
            var cars = _car.GetAll();
            return Ok(cars);
        }

        [HttpGet("bestseller")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetBestSeller()
        {
            var cars = _car.GetBestSeller();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Car))]
        [ProducesResponseType(400)]
        public IActionResult GetCar(int? id) {
            if (id == null) return BadRequest("id is required");
            if (!_car.ExistCar(id)) return NotFound();

            var car = _car.Get(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(car);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Admin")]
        public IActionResult PostCar(CarDto car)
        {

            if (car == null)
            {
                return BadRequest(ModelState);
            }
            int BrandId = car.BrandId;
            if (!ModelState.IsValid) {
               
                return NotFound();
            }

            var CarMap = _mapper.Map<Car>(car);

            CarMap.Brand = _brand.Get(BrandId);
            if (!_car.Add(CarMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        // public IActionResult PutCar([FromQuery] int? id, [FromBody] CarDto EditedCar)
        public IActionResult PutCar( int? id,  CarDto EditedCar)
        {
           // if (id == null) return BadRequest("id is required");
            if (id != EditedCar.ID) return BadRequest("id is not match");
            if (!_car.ExistCar(id)) return NotFound(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var carMap = _mapper.Map<Car>(EditedCar);
            Brand brand = _brand.Get(EditedCar.BrandId);
            if (brand == null) return BadRequest("Brand is not exist");
            carMap.Brand = brand;
            if (!_car.UpdateCar(carMap))
            {
                ModelState.AddModelError("", "error while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCar(int? id)
        {
            if (id == null) return BadRequest("id is requird");
            if (!_car.ExistCar(id)) return NotFound();
            _car.Delete(id);
            return Ok();
        }
        [HttpGet("{carId}/CarPhotos")]
        public IActionResult GetCarPhotos(int? carId)
        {
            if (carId == null) return BadRequest("id is requird");
            if (!_car.ExistCar(carId)) return NotFound();
            IEnumerable<Photo> photos= _car.getPhotes(carId);
            return Ok(photos);

        }
        [HttpGet("{id}/Details")]
        public IActionResult GetCarDetails(int? id)
        {
            if (id == null) return BadRequest("id is requird");
            if (!_car.ExistCar(id)) return NotFound();
            IEnumerable<Detail> details = _car.getDetails(id);
            return Ok(details);
        }

        //GetBrancesContainCar
        [HttpGet("{id}/BrancesContainCar")]
        public IActionResult GetBrancesContainCar(int? id)
        {
            if (id == null) return BadRequest("id is requird");
            if (!_car.ExistCar(id)) return NotFound();
            ICollection<Branch> branches = _car.GetBrancesContainCar(id);
            return Ok(branches);
        }

        [HttpGet("{id}/CarInBranch")]
        public IActionResult CarInBranch(int? id)
        {
            if (id == null) return BadRequest("id is requird");
         
            ICollection<Car> Cars = _car.GetCarinBranch(id);
            return Ok(Cars);
        }


    }
}
