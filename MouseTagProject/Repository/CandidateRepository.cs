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
        public void AddCandidate(AddCandidateDto candidate)
        {
            var dates = new List<UserDate>();
            dates.Add(new UserDate()
            {
                Date = candidate.WhenWasContacted
            });
            var newCandidate = new Candidate
            {
                WhenWasContacted = dates,
                Name = candidate.Name,
                Surname = candidate.Surname,
                Linkedin = candidate.Linkedin,
                Comment = candidate.Comment,
                Available = candidate.Available,
                WillBeContacted = candidate.WillBeContacted,

                Technologies = candidate.TechnologyIds.Select(x => new CandidateTechnology()
                {
                    TechnologyId = x
                }).ToList()
            };
              
            _candidateContext.Candidates.Add(newCandidate);
            _candidateContext.SaveChanges();
        }

        public void DeleteCandidate(int id)
        {
            var candidate = _candidateContext.Candidates.FirstOrDefault(x => x.Id == id);
            _candidateContext.Candidates.Remove(candidate);
            _candidateContext.SaveChanges();
        }

        public CandidateListItemDto GetCandidate(int id)
        {
            var candidateModel = _candidateContext.Candidates.Where(x => x.Id == id).Include(x => x.WhenWasContacted).Include(x => x.Technologies).ThenInclude(x => x.Technology).FirstOrDefault();
            if (candidateModel != null)
            {
                var candidateListItemDto = new CandidateListItemDto()
                {
                    Id = candidateModel.Id,
                    WhenWasContacted = candidateModel.WhenWasContacted.Select(x => x.Date),
                    Name = candidateModel.Name,
                    Surname = candidateModel.Surname,
                    Linkedin = candidateModel.Linkedin,
                    Available = candidateModel.Available,
                    Technologies = candidateModel.Technologies.Select(x => new TechnologyDto()
                    {
                        Id = x.TechnologyId,
                        TechnologyName = x.Technology.TechnologyName

                    }),
                    WillBeContacted = candidateModel.WillBeContacted
                };

                return candidateListItemDto;
            }
            return null;
        }

        public List<CandidateListItemDto> GetCandidates()
        {
            var candidateModel = _candidateContext.Candidates.Include(x => x.WhenWasContacted).Include(x => x.Technologies).ThenInclude(x => x.Technology).ToList();
            var candidateDto = new List<CandidateListItemDto>();
            foreach (var candidate in candidateModel)
            {
                candidateDto.Add(new CandidateListItemDto()
                {
                    Id = candidate.Id,
                    WhenWasContacted = candidate.WhenWasContacted.Select(x => x.Date),
                    Name = candidate.Name,
                    Surname = candidate.Surname,
                    Linkedin = candidate.Linkedin,
                    Available = candidate.Available,
                    Technologies = candidate.Technologies.Select(x => new TechnologyDto()
                    {
                        Id = x.TechnologyId,
                        TechnologyName = x.Technology.TechnologyName

                    }),
                    WillBeContacted = candidate.WillBeContacted
                });
            };
            return candidateDto;
        }

        public List<Candidate> GetCandidatesReminder()
        {
            return _candidateContext.Candidates.Include(x => x.WhenWasContacted).ToList();
        }

        public List<CandidateListItemDto> UpdateCandidate(int id, AddCandidateDto updatedCandidate)
        {
            var candidateModel = _candidateContext.Candidates.Where(x => x.Id == id).Include(x => x.WhenWasContacted).Include(x => x.Technologies).ThenInclude(x => x.Technology).FirstOrDefault();
            candidateModel.Name = updatedCandidate.Name;
            //candidateModel.WhenWasContacted = updatedCandidate.WhenWasContacted.Select(x => new UserDate()
            //{
            //    Date = x.Date
            //}).ToList();
            candidateModel.Surname = updatedCandidate.Surname;
            candidateModel.Linkedin = updatedCandidate.Linkedin;
            candidateModel.Comment = updatedCandidate.Comment;
            candidateModel.Available = updatedCandidate.Available;
            candidateModel.Technologies = updatedCandidate.TechnologyIds.Select(x => new CandidateTechnology()
            {
                TechnologyId = x
            }).ToList();
            candidateModel.WillBeContacted = updatedCandidate.WillBeContacted;

            _candidateContext.SaveChanges();

            return GetCandidates();
        }
    }
}
