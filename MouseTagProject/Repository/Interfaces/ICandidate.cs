using MouseTagProject.Models;

namespace MouseTagProject.Repository.Interfaces
{
    public interface ICandidate
    {
        List<CandidateListItemDto> GetCandidates();

        List<Candidate> GetCandidatesReminder();
        CandidateListItemDto GetCandidate(int id);

        void AddCandidate(AddCandidateDto candidate);

        List<CandidateListItemDto> UpdateCandidate(int id, AddCandidateDto candidate);

        void DeleteCandidate(int id);

    }
}
