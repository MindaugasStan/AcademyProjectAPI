namespace MouseTagProject.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int MyProperty { get; set; }
        public string Linkedin { get; set; }
        public string Comment { get; set; }
        public bool Available { get; set; }
        public List<Technology> Technologies { get; set; }
        public List<UserDate> Dates { get; set; }
        public Guid Guid { get; set; }
    }
}
