using System;
using System.Collections.Generic;
using AutoMapper;
using DAL.Repository;

namespace BLL.Services
{
    public class ManagerService: IService<DAL.Manager, BLL.Manager>
    {
        private readonly IGenericRepository<DAL.Manager> _managerRepository;
        private readonly IMapper _mapper;

        public ManagerService(IMapper mapper)
        {
            _managerRepository = new GenericRepository<DAL.Manager>();
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
            _managerRepository.Delete(Entity);
        }

        public void Remove(IEnumerable<DAL.Manager> Entities)
        {
            _managerRepository.Delete(Entities);
        }

        public void Add(DAL.Manager Entity)
        {
            _managerRepository.Add(Entity);
        }

        public void Add(IEnumerable<DAL.Manager> Entities)
        {
            _managerRepository.Add(Entities);
        }

        public void SaveChanges()
        {
            _managerRepository.SaveChanges();
        }

        public DAL.Manager Find(Guid managerId)
        {
            return _managerRepository.Find(managerId);
        }
    }
}
