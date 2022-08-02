namespace MouseTagProject.Models
{
    public class UserDate
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public bool WasContacted { get; set; }
        public int CandidateId { get; set; }
        public int TestData { get; set; }
    }
}
