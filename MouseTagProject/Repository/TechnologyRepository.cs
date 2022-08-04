using MouseTagProject.Context;
using MouseTagProject.Models;
using MouseTagProject.Repository.Interfaces;

namespace MouseTagProject.Repository
{
    public class TechnologyRepository : ITechnology
    {

        private MouseTagProjectContext _technologyContext;
        public TechnologyRepository(MouseTagProjectContext technologyContext)
        {
            _technologyContext = technologyContext;
        }

        public void AddTechnology(Technology technology)
        {
            _technologyContext.Technologies.Add(technology);
            _technologyContext.SaveChanges();
        }

        public void DeleteTechnology(Technology technology)
        {
            _technologyContext.Technologies.Remove(technology);
            _technologyContext.SaveChanges();
        }

        public List<Technology> GetTechnologies()
        {
            return _technologyContext.Technologies.ToList();
        }

        public Technology GetTechnology(int id)
        {
            return _technologyContext.Technologies.Where(x => x.Id == id).FirstOrDefault();
        }

        public Technology UpdateTechnology(Technology technology)
        {
            var existingTechnology = _technologyContext.Technologies.Find(technology.Id);
            if(existingTechnology != null)
            {
                existingTechnology.TechnologyName = technology.TechnologyName;
                _technologyContext.Technologies.Update(existingTechnology);
                _technologyContext.SaveChanges();
                return existingTechnology;
            }
            return null;

        }
    }
}
