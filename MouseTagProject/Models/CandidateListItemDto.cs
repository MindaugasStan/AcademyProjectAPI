namespace MouseTagProject.Models
{
    public class CandidateListItemDto
    {
        public int Id { get; set; }
        public IEnumerable<DateTime> WhenWasContacted { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Linkedin { get; set; }
        public string Comment { get; set; }
        public bool Available { get; set; }
        public IEnumerable<TechnologyDto> Technologies { get; set; }
        public DateTime WillBeContacted { get; set; }

    }
}
