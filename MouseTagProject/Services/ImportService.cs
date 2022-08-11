using MouseTagProject.Context;
using MouseTagProject.DTOs;
using MouseTagProject.Models;

namespace MouseTagProject.Services
{
    public class ImportService
    {
        public List<Candidate> ImportFromExel(List<ImportCandidateDto> importCandidates)
        {
            List<Candidate> candidates = new List<Candidate>();

            foreach (var importCandidate in importCandidates)
            {
                Candidate candidate = new Candidate();

                List<DateTime> dateTime = new List<DateTime>();

                if (importCandidate.DateListAsInt.GetType() == typeof(System.Int32))
                {
                    var dateInt = Convert.ToInt32(importCandidate.DateListAsInt);
                    DateTime dt = DateTime.FromOADate(dateInt);
                    dateTime = new List<DateTime>() { dt };
                }
                if (importCandidate.DateListAsInt.GetType() == typeof(System.String))
                {
                    var splitedDate = importCandidate.DateListAsInt.Split(new char[] { ' ', ',', '.' }).ToList();

                    var dateInt = 0;
                    splitedDate.ForEach(date =>
                    {
                        dateInt = Convert.ToInt32(date);
                        DateTime dt = DateTime.FromOADate(dateInt);
                        var dateTime = new List<DateTime>() { dt };
                    });
                }

                candidate.Name = importCandidate.Name;
                candidate.Surname = importCandidate.Surname;
                candidate.Linkedin = importCandidate.Linkedin;
                candidate.Comment = importCandidate.Comment;


                var technologies = importCandidate.TechnologyListAsString.Split(new char[] { ' ', ',' });
                IEnumerable<string> strings = technologies;
                //  candidate.Technologies = 
                //candidate.WhenWasContacted = 
                // candidates.Add(candidate);
            }
            return candidates;
        }
    }
}
