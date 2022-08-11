using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MouseTagProject.DTOs;
using MouseTagProject.Services;

namespace MouseTagProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly ImportService _importService;

        public ImportController(ImportService importService)
        {
            _importService = importService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Import(ImportCandidateDto importCandidateDto)
        {
            // var results = _importService.ImportFromExel(candidateList);
            return Ok();
        }
    }
}
