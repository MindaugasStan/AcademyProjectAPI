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
        public IActionResult Import(ImportCandidateDto importCandidateDto)
        {
            // List<ImportCandidateDto> candidateList = new List<ImportCandidateDto>();
            // candidateList.Add(new ImportCandidateDto() { DateListAsInt = "54452 45675 45668", Name = "Tomas", Surname = "Kulkauskas", Comment = "Geras program.", Linkedin = "LinkedinUrl", TechnologyListAsString = ".Net Java" });
            // candidateList.Add(new ImportCandidateDto() { DateListAsInt = "45252", Name = "Marius", Surname = "Bernotas", Comment = "Neblogas .net programuotojas", Linkedin = "LinkedinUrl mano", TechnologyListAsString = "PHP JavaScript" });
            // var results = _importService.ImportFromExel(candidateList);

            return Ok();
        }
    }
}
