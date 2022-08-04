using MouseTagProject.Context;
using MouseTagProject.Models;
using MouseTagProject.Repository.Interfaces;

namespace MouseTagProject.Repository
{
    public class CandidateSql : ICandidate
    {

        private MouseTagProjectContext _candidateContext;
        public CandidateSql(MouseTagProjectContext candidateContext)
        {
            _candidateContext = candidateContext;
        }
        public void AddCandidate(Candidate candidate)
        {
            _candidateContext.Candidates.Add(candidate);
            _candidateContext.SaveChanges();
        }

        public void DeleteCandidate(Candidate candidate)
        {
            _candidateContext.Candidates.Remove(candidate);
            _candidateContext.SaveChanges();
        }

        public Candidate GetCandidate(int id)
        {
            return _candidateContext.Candidates.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Candidate> GetCandidates()
        {
           return _candidateContext.Candidates.ToList();
        }

        public Candidate UpdateCandidate(Candidate candidate)
        {
            throw new NotImplementedException();
        }
    }
}
