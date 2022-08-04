using MouseTagProject.Models;

namespace MouseTagProject.Repository.Interfaces
{
    public interface ICandidate
    {
        List<Candidate> GetCandidates();

        Candidate GetCandidate(int id);

        void AddCandidate(Candidate candidate);

        Candidate UpdateCandidate(Candidate candidate);

        void DeleteCandidate(Candidate candidate);

    }
}
