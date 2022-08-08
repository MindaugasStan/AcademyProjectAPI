namespace MouseTagProject.Models
{
    public class CandidateTechnology
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int TechnologyId { get; set; }
        public Candidate Candidate { get; set; }
        public Technology Technology { get; set; }

    }
}
