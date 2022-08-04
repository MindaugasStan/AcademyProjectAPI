using MouseTagProject.Models;

namespace MouseTagProject.Repository.Interfaces
{
    public interface ITechnology
    {
        List<Technology> GetTechnologies();

        Technology GetTechnology(int id);
        void AddTechnology(Technology technology);
        Technology UpdateTechnology(Technology technology);
        void DeleteTechnology(Technology technology);
    }
}
