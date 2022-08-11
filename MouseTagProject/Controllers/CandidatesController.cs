using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MouseTagProject.Models;
using MouseTagProject.Repository.Interfaces;

namespace MouseTagProject.Controllers
{
    public class CandidatesController : ControllerBase
    {
        private ICandidate _candidate;
        public CandidatesController(ICandidate candidate)
        {
            _candidate = candidate;
        }

        [HttpGet]
        [Route("api/[controller]")]
        [Authorize]
        public IActionResult GetCandidates()
        {
            return Ok(_candidate.GetCandidates());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        [Authorize]
        public IActionResult GetCandidates(int id)
        {
            var candidate = _candidate.GetCandidate(id);
            if (candidate != null)
            {
                return Ok(candidate);
            }
            return NotFound();

        }

        [HttpPost]
        [Route("api/[controller]")]
        [Authorize]
        public IActionResult AddCandidate([FromBody] AddCandidateDto candidate)
        {
            _candidate.AddCandidate(candidate);
            return Ok(candidate);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCandidate(int id)
        {
            //var candidate = _candidate.GetCandidate(id);
            var candidate = _candidate.GetCandidate(id);
            if (candidate != null)
            {
                _candidate.DeleteCandidate(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        [Route("api/[controller]/{id}")]
        [Authorize]
        public IActionResult EditCandidate(int id, [FromBody] AddCandidateDto candidate)
        {
            var existingCandidate = _candidate.GetCandidate(id);
            if (existingCandidate != null)
            {
                var newCandidate = _candidate.UpdateCandidate(id, candidate);
                return Ok(newCandidate);
            }
            return NotFound();
        }
    }
}
