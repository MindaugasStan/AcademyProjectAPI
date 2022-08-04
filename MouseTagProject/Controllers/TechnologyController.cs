using Microsoft.AspNetCore.Mvc;
using MouseTagProject.Models;
using MouseTagProject.Repository.Interfaces;

namespace MouseTagProject.Controllers
{
    public class TechnologyController : Controller
    {

        private ITechnology _technology;
        public TechnologyController(ITechnology technology)
        {
            _technology = technology;
        }
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetTechnologies()
        {
            return Ok(_technology.GetTechnologies());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetTechnology(int id)
        {
            var technology = _technology.GetTechnology(id);
            if (technology != null)
            {
                return Ok(technology);
            }
            return NotFound();

        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult AddTechnology(Technology technology)
        {
            _technology.AddTechnology(technology);
            return Ok(technology);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteTechnology(int id)
        {
            var technology = _technology.GetTechnology(id);
            if (technology != null)
            {
                _technology.DeleteTechnology(technology);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult EditTechnology(int id, Technology technology)
        {
            var existingTechnology = _technology.GetTechnology(id);
            if (existingTechnology != null)
            {
                var newTechnology = _technology.UpdateTechnology(technology);
                return Ok(newTechnology);
            }
            return NotFound();
        }
    }
}
