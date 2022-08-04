using Microsoft.EntityFrameworkCore;
using MouseTagProject.Context;
using MouseTagProject.Models;
using MouseTagProject.Repository.Interfaces;

namespace MouseTagProject.Repository
{
    public class CandidateRepository : ICandidate
    {

        private MouseTagProjectContext _candidateContext;
        public CandidateRepository(MouseTagProjectContext candidateContext)
        {
            _candidateContext = candidateContext;
        }
        public void AddCandidate(Candidate candidate)
        {
            var technologyId = candidate.Technologies[0].Id;
            var technology = _candidateContext.Technologies.Where(x => x.Id == technologyId).FirstOrDefault();
            if (technology != null)
            {
                candidate.Technologies[0] = technology;
            }
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
            return _candidateContext.Candidates.Where(x => x.Id == id).Include(x => x.WhenWasContacted).Include(x => x.Technologies).FirstOrDefault();
        }

        public List<Candidate> GetCandidates()
        {
            return _candidateContext.Candidates.Include(x => x.WhenWasContacted).Include(x => x.Technologies).ToList();
        }

        public Candidate UpdateCandidate(Candidate candidate)
        {
            throw new NotImplementedException();
        }
    }
}
