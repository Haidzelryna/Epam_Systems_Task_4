using System.Collections.Generic;
using AutoMapper;
using DAL;
using DAL.Repository;

namespace BLL.Services
{
    public class ManagerService: IService<DAL.Manager, BLL.Manager>
    {
        private readonly IGenericRepository<DAL.Manager> _cmanagerRepository;
        private readonly IMapper _mapper;

        public ManagerService(IGenericRepository<DAL.Manager> cmanagerRepository, IMapper mapper)
        {
            _cmanagerRepository = cmanagerRepository;
            _mapper = mapper;
        }

        public DAL.Manager Get(BLL.Manager Entity)
        {
            return _mapper.Map<DAL.Manager>(Entity);
        }

        public IEnumerable<DAL.Manager> Get(IEnumerable<BLL.Manager> Entities)
        {
            return _mapper.Map<IEnumerable<DAL.Manager>>(Entities);
        }

        public void Remove(DAL.Manager Entity)
        {
            _cmanagerRepository.Delete(Entity);
        }

        public void Remove(IEnumerable<DAL.Manager> Entities)
        {
            _cmanagerRepository.Delete(Entities);
        }

        public void Add(DAL.Manager Entity)
        {
            _cmanagerRepository.Add(Entity);
        }

        public void Add(IEnumerable<DAL.Manager> Entities)
        {
            _cmanagerRepository.Add(Entities);
        }

        public void SaveChanges()
        {
            _cmanagerRepository.SaveChanges();
        }
    }
}
