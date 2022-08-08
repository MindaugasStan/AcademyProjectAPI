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

        public void AddTechnology(TechnologyDto technology)
        {
            var technologyModel = new Technology()
            {
                Id = technology.Id,
                TechnologyName = technology.TechnologyName
            };
            _technologyContext.Technologies.Add(technologyModel);
            _technologyContext.SaveChanges();
        }

        public void DeleteTechnology(int id)
        {
            var technology = _technologyContext.Technologies.FirstOrDefault(x => x.Id == id);
            _technologyContext.Technologies.Remove(technology);
            _technologyContext.SaveChanges();
        }

        public List<TechnologyDto> GetTechnologies()
        {
            var technologyModel = _technologyContext.Technologies.ToList();
            var technologyDto = new List<TechnologyDto>();
            foreach(var technology in technologyModel)
            {
                technologyDto.Add(new TechnologyDto()
                {
                    Id = technology.Id,
                    TechnologyName = technology.TechnologyName
                });
            };

            return technologyDto;
                
        }

        public TechnologyDto GetTechnology(int id)
        {
            var technologyModel = _technologyContext.Technologies.Where(x => x.Id == id).FirstOrDefault();
            if(technologyModel != null)
            {
                var technologyDto = new TechnologyDto()
                {
                    Id = technologyModel.Id,
                    TechnologyName = technologyModel.TechnologyName
                };
            return technologyDto;
            }
            return null;
        }

        public List<TechnologyDto> UpdateTechnology(int id, TechnologyDto technology)
        {
            var existingTechnology = _technologyContext.Technologies.Find(id);
            if(existingTechnology != null)
            {
                existingTechnology.Id = technology.Id;
                existingTechnology.TechnologyName = technology.TechnologyName;
                _technologyContext.Technologies.Update(existingTechnology);
                _technologyContext.SaveChanges();
                return GetTechnologies();
            }
            return null;

        }
    }
}
