using MouseTagProject.Models;

namespace MouseTagProject.Interfaces
{
    public interface IEmailService
    {
        string GenerateLetter(List<Candidate> candidates);
        bool SendEmail(string letterHTML);
    }
}
