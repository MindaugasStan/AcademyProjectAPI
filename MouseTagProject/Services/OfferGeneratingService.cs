using Spire.Doc;
using Spire.Doc.Documents;
using System.Drawing;

namespace MouseTagProject.Services
{
    public class OfferGeneratingService
    {
        public string GenerateFile(string name, string surname)
        {
            var upperNames = name.ToUpper() + ' ' + surname.ToUpper();
            var upperFirstLetters = char.ToUpper(name[0]) + name.Substring(1) + ' ' + char.ToUpper(surname[0]) + surname.Substring(1);
            var fileName = "Job_offer_Xplicity_" + char.ToUpper(name[0]) + name.Substring(1) + "_" + char.ToUpper(surname[0]) + surname.Substring(1);
            var date = DateTime.Now;
            string changedDate = date.ToString("dd MMMM yyyy").Insert(2, GetDaySuffix(date.Day));

            Document doc = new Document(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Job_offer_Xplicity_ Vardenis Pavardenis.docx"));
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
            doc.SaveToFile(fileName);
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
