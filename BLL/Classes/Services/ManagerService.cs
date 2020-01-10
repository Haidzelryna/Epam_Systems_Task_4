using System;
using System.Collections.Generic;
using AutoMapper;
using DAL.Repository;
using System.Threading.Tasks;
using BLL.Classes.Services;

namespace BLL.Services
{
    public class ManagerService: IService<DAL.Manager, BLL.Manager>
    {
        private readonly IGenericRepository<DAL.Manager> _managerRepository;
        private readonly IMapper _mapper;

        private static readonly SemaphoreLocker _locker = new SemaphoreLocker();

        public ManagerService(IMapper mapper, IGenericRepository<DAL.Manager> managerRepository)
        {
            _managerRepository = managerRepository;
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

        public async Task<DAL.Manager> FindAsync(Guid managerId)
        {
            DAL.Manager result = new DAL.Manager();
            await _locker.LockAsync(async () =>
            {
                result = await _managerRepository.FindAsync(managerId);
            });
            return result;
        }
    }
}
