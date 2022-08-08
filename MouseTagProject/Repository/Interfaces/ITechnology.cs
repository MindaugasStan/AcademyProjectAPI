using MouseTagProject.Models;

namespace MouseTagProject.Repository.Interfaces
{
    public interface ITechnology
    {
        List<TechnologyDto> GetTechnologies();

        TechnologyDto GetTechnology(int id);
        void AddTechnology(TechnologyDto technology);
        List<TechnologyDto> UpdateTechnology(int id, TechnologyDto technology);
        void DeleteTechnology(int id);
    }
}
