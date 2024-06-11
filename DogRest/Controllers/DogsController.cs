using DogLib;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DogRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly DogsRepository _dogsRepository;
        public DogsController(DogsRepository dogsRepository)
        {
            _dogsRepository = dogsRepository;
        }
        // GET: api/<DogsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Dog>> Get([FromQuery] string? Name = null, int Age = 0)
        {
            IEnumerable<Dog> dogs = _dogsRepository.GetAll(Name, Age);
            if(dogs == null)
            {
                return NotFound("Dogs collection is null");
            }
            else if(!dogs.Any()) 
            {
                return NoContent();
            }
            else
            {
                return Ok(dogs);//200
            }
        }

        // GET api/<DogsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            Dog? dog = _dogsRepository.GetById(id);
            if(dog == null)
            {
                return NotFound("Dog not found");
            }
            else
            {
                return Ok(dog);
            }
        }

        // POST api/<DogsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Dog> Post([FromBody] Dog value)
        {
            Dog dog = _dogsRepository.Add(value);
            return CreatedAtAction(nameof(Get), new { id = dog.Id }, dog);
        }

        // DELETE api/<DogsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Dog> Delete(int id)
        {
            Dog? dog = _dogsRepository.Delete(id);
            if(dog == null)
            {
                return NotFound("Dog not found");
            }
                return Ok(dog);
        }
    }
}
