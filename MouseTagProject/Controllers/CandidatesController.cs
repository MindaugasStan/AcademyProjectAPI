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
        public IActionResult GetCandidates()
        {
            return Ok(_candidate.GetCandidates());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetCandidates(int id)
        {
            var candidate = _candidate.GetCandidate(id);
            if(candidate != null)
            {
                return Ok(candidate);
            }
            return NotFound();
            
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult AddCandidate([FromBody] Candidate candidate)
        {
            _candidate.AddCandidate(candidate);
            return Ok(candidate);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteCandidate(int id)
        {
            var candidate = _candidate.GetCandidate(id);
            if (candidate != null)
            {
                _candidate.DeleteCandidate(candidate);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult EditCandidate(int id, [FromBody] Candidate candidate)
        {
            var existingCandidate = _candidate.GetCandidate(id);
            if (existingCandidate != null)
            {
                candidate.Id = existingCandidate.Id;
                var newCandidate = _candidate.UpdateCandidate(candidate);
                return Ok(newCandidate);
            }
            return NotFound();
        }
    }
}
