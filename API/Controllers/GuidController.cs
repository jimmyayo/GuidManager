using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    public class GuidController : Controller
    {
        private readonly IGuidRepository _guidRepository;

        public GuidController(IGuidRepository guidRepository)
        {
            _guidRepository = guidRepository;
        }

        [HttpGet]
        public IActionResult GetAllValid()
        {
            var items = _guidRepository.GetAllValid();
            return Ok(items);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(string guid)
        {
            var guidToFind = _guidRepository.Get(guid);
            if (guidToFind is null)
                return NotFound();

            return Ok(guidToFind);
        }

        [HttpPost]
        public IActionResult Create(MyGuid myGuid)
        {
            _guidRepository.Add(myGuid);
            
            return Ok(myGuid);
        }

        [HttpPut]
        public IActionResult Update(MyGuid myGuid)
        {
            _guidRepository.Update(myGuid);
            
            return Ok(myGuid);
        }

        [HttpDelete]
        public IActionResult Delete(string guid)
        {
            _guidRepository.Remove(guid);
            return Ok();
        }
    }
}