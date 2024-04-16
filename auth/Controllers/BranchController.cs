using AutoMapper;
using BusinessObjects.Dto;
using BusinessObjects.Entities;
using BusinessObjects.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranch _branch;
        private readonly IMapper _mapper;
        private readonly ICar _car;
        public BranchController(IBranch branch,IMapper mapper, ICar car) 
        {
            _branch = branch;
            _mapper = mapper;
            _car = car;
        }
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Branch>))]
        public IActionResult GetAll()
        {
            IEnumerable<Branch> branches= _branch.GetAll();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(branches);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200,Type=typeof(Branch))]
        public IActionResult Get(int? id)
        {
            if (id == null) return BadRequest("id is required");
            if (!_branch.ExistBranch(id)) return NotFound();

            Branch branch = _branch.Get(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(branch);
        } 
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PostBranch([FromBody] Branch branch)
        {
            if (branch == null) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_branch.Add(branch))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPut("{id}")]
        public IActionResult PutBranch(int? id,[FromBody] Branch branch)
        {
            if (id == null) return BadRequest("id is required");
            if (id != branch.Id) return BadRequest("id is not match");
            if (branch == null) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            if (!_branch.UpdateBranch(branch))

            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete]
        public IActionResult DeleteBranch(int? id)
        {
            if (id == null) return BadRequest("id is required");
            if (!_branch.ExistBranch(id)) return NotFound();
            Branch branch = _branch.Get(id);
            _branch.Delete(branch);
            return NoContent();
        }

          [HttpGet("{id}/BrancesContainCar")]
        public IActionResult GetBrancesContainCar(int? id)
        {
            if (id == null) return BadRequest("id is requird");
         //   if (!_car.ExistCar(id)) return NotFound();
            ICollection<Branch> branches = _car.GetBrancesContainCar(id);
            return Ok(branches);
        }

        [HttpGet("{id}/CarInBranch")]
        public IActionResult CarInBranch(int? id)
        {
            if (id == null) return BadRequest("id is requird");
         
            ICollection<Car> Cars = _branch.GetCarinBranch(id);
            return Ok(Cars);
        }
    }

}
