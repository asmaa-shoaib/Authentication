using AutoMapper;
using BusinessObjects.Dto;
using BusinessObjects.Entities;
using BusinessObjects.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace authApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailController : ControllerBase
    {
        //private readonly IGenericRepository<Car> _carRepository;
        private readonly ICar _car;
        private readonly IDetail _detail;
        private readonly IMapper _mapper;
        // private readonly Data_Base _data_Base;
        public DetailController(ICar car, IMapper mapper, IDetail detail)
        {
            _car = car;
            _detail = detail;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Detail>))]
        public IActionResult GetDetails()
        {
            var Details = _detail.GetAll();
            return Ok(Details);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Detail))]
        [ProducesResponseType(400)]
        public IActionResult GetDetail(int? id)
        {
            if (id == null) return BadRequest("id is required");
            if (!_detail.ExistDetail(id)) return NotFound();

            var detail = _detail.Get(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(detail);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PostCar( [FromBody] DetailDto detail)
        {
            if (detail == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var detailMap = _mapper.Map<Detail>(detail);
            if (!_detail.Add(detailMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }


        [HttpPut("{id}")]
        public IActionResult PutDetail(int? id, DetailDto EditedDetail)
        {
            if (id == null) return BadRequest("id is required");
            if (id != EditedDetail.Id) return BadRequest("id is not match");
            if (!_detail.ExistDetail(id)) return NotFound(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var detailMap = _mapper.Map<Detail>(EditedDetail);
            if (!_detail.UpdateDetail(detailMap))
            {
                ModelState.AddModelError("", "error while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteDetail(int? id)
        {
            if (id == null) return BadRequest("id is requird");
            if (!_detail.ExistDetail(id)) return NotFound();
            _detail.Delete(_detail.Get(id));
            return Ok();
        }
        [HttpGet("{carId}/CarDetails")]
        public IActionResult GetCarDetails(int? carId)
        {
            if (carId == null) return BadRequest("id is requird");
            if (!_car.ExistCar(carId)) return NotFound("car is no exist");
            Detail detail = _detail.GetCarDetail(carId);
            if (detail == null) return NotFound();
            return Ok(detail);

        }
     

    }
}