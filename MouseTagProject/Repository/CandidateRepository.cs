using Microsoft.EntityFrameworkCore;
using MouseTagProject.Context;
using MouseTagProject.Models;
using MouseTagProject.Repository.Interfaces;
using Spire.Doc;
using Spire.Doc.Documents;
using System.Drawing;

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
                    Comment = candidateModel.Comment,
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
                    Comment = candidate.Comment,
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
            var modelDate = candidateModel.WhenWasContacted;
            bool foundIt = false;
            var userDates = new UserDate()
            {
                Date = updatedCandidate.WhenWasContacted
            };
            foreach(UserDate date in modelDate)
            {
                var value = DateTime.Compare(date.Date.Date, userDates.Date.Date);
                if(value == 0)
                {
                    foundIt = true;
                }    
            }
            candidateModel.Name = updatedCandidate.Name;
            if(!foundIt)
            {
                candidateModel.WhenWasContacted = candidateModel.WhenWasContacted.Append(userDates).ToList();
            }
            candidateModel.Surname = updatedCandidate.Surname;
            candidateModel.Linkedin = updatedCandidate.Linkedin;
            candidateModel.Comment = updatedCandidate.Comment;
            candidateModel.Available = updatedCandidate.Available;
            candidateModel.Technologies = updatedCandidate.TechnologyIds.Select(x => new CandidateTechnology()
            {
                TechnologyId = x
            }).ToList();
            candidateModel.WillBeContacted = updatedCandidate.WillBeContacted;

            _candidateContext.Candidates.Update(candidateModel);
            _candidateContext.SaveChanges();

            return GetCandidates();
        }

        public string GenerateFile(CandidateListItemDto candidate)
        {
            var upperNames = candidate.Name.ToUpper() + ' ' + candidate.Surname.ToUpper();
            var upperFirstLetters = char.ToUpper(candidate.Name[0]) + candidate.Name.Substring(1) + ' ' + char.ToUpper(candidate.Surname[0]) + candidate.Surname.Substring(1);
            var fileName = "Job_offer_Xplicity_" + char.ToUpper(candidate.Name[0]) + candidate.Name.Substring(1) + "_" + char.ToUpper(candidate.Surname[0]) + candidate.Surname.Substring(1);
            var date = DateTime.Now;
            string changedDate = date.ToString("dd MMMM yyyy").Insert(2, GetDaySuffix(date.Day));
            var filePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Job_offer_Xplicity_ Vardenis Pavardenis.docx");
            Document doc = new Document("D:\\Job_offer_Xplicity_ Vardenis Pavardenis.docx");
            TextSelection[] text = doc.FindAllString("Vardenis Pavardenis", false, true);
            TextSelection[] textDate = doc.FindAllString("9th February 2022", true, true);
            foreach (TextSelection s in text)
            {
                s.GetAsOneRange().CharacterFormat.HighlightColor = Color.White;

            }
            foreach (TextSelection s in textDate)
            {
                s.GetAsOneRange().CharacterFormat.HighlightColor = Color.White;
            }
            doc.Replace("Vardenis Pavardenis", upperFirstLetters, true, true);
            doc.Replace("VARDENIS PAVARDENIS", upperNames, true, true);
            doc.Replace("9th February 2022", changedDate, true, true); ;
            doc.SaveToFile(fileName + ".docx");
            return fileName;
        }
        static string GetDaySuffix(int day)
        {
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }
    }
}
