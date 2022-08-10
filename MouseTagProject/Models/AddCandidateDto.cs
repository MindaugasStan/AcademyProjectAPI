namespace MouseTagProject.Models
{
    public class AddCandidateDto
    {
        public DateTime WhenWasContacted { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Linkedin { get; set; }
        public string Comment { get; set; } = "";
        public bool Available { get; set; }
        public List<int> TechnologyIds { get; set; }
        public DateTime WillBeContacted { get; set; }
    }
}
