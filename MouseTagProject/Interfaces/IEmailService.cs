using MouseTagProject.Models;

namespace MouseTagProject.Interfaces
{
    public interface IEmailService
    {
        string GenerateLetter(List<CandidateListItemDto> candidates);
        bool SendEmail(string letterHTML);
    }
}
