using System;
using System.Collections.Generic;
using AutoMapper;
using DAL.Repository;
using System.Threading.Tasks;
using System.Linq;
using BLL.Classes.Services;

namespace BLL.Services
{
    public class ClientService: IService<DAL.Client, BLL.Client>
    {
        private readonly IGenericRepository<DAL.Client> _clientRepository;
        private readonly IMapper _mapper;

        private static readonly SemaphoreLocker _locker = new SemaphoreLocker();

        static object locker = new object();

        public ClientService(IMapper mapper)
        {
            _clientRepository = new GenericRepository<DAL.Client>();
            _mapper = mapper;
        }

        public DAL.Client Get(BLL.Client Entity)
        {
            return _mapper.Map<DAL.Client>(Entity);
        }

        public IEnumerable<DAL.Client> Get(IEnumerable<BLL.Client> Entities)
        {
            return _mapper.Map<IEnumerable<DAL.Client>>(Entities);
        }

        public async Task<IEnumerable<DAL.Client>> GetAll()
        {
            IEnumerable<DAL.Client> result = Enumerable.Empty<DAL.Client>();
            await _locker.LockAsync(async () =>
            {
                result = await _clientRepository.GetAllAsync();
            });

            return result;
        }

        public async Task<bool> Check(IEnumerable<Guid> clientsCheck)
        {
            IEnumerable<DAL.Client> clients = await GetAll();
            foreach (Guid clientId in clientsCheck)
            {
                if (clients.Select(c => c.Id).ToList().Contains(clientId) == false)
                {
                    return false;
                };
            }
            return true;
        }

        //для сопоставления Id - name
        public async Task<bool> CheckNameId(IEnumerable<string> clientsCheck)
        {
            IEnumerable<DAL.Client> clients = await GetAll();
            foreach (string clientName in clientsCheck)
            {
                //if (clients.Select(c => c.Na).ToList().Contains(clientId) == false)
                //{
                //    return false;
                //};
            }
            return true;
        }

        public void Remove(DAL.Client Entity)
        {
            _clientRepository.Delete(Entity);
        }

        public void Remove(IEnumerable<DAL.Client> Entities)
        {
            _clientRepository.Delete(Entities);
        }

        public void Add(DAL.Client Entity)
        {
            _clientRepository.Add(Entity);
        }

        public void Add(IEnumerable<DAL.Client> Entities)
        {
            _clientRepository.Add(Entities);
        }

        public void SaveChanges()
        {
            _clientRepository.SaveChanges();
        }

        public DAL.Client Find(Guid clientId)
        {
            return _clientRepository.Find(clientId);
        }
    }
}
