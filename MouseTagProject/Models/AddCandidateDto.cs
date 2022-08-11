namespace MouseTagProject.Models
{
    public class AddCandidateDto
    {
        public DateTime WhenWasContacted { get; set; } = DateTime.MinValue;
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Linkedin { get; set; }
        public string Comment { get; set; } = "";
        public bool Available { get; set; }
        public List<int> TechnologyIds { get; set; } = new List<int>();
        public DateTime WillBeContacted { get; set; } = DateTime.MinValue;
    }
}
