using System.Collections.Generic;
using AutoMapper;
using DAL;
using DAL.Repository;

namespace BLL.Services
{
    public class ClientService: IService<DAL.Client, BLL.Client>
    {
        private readonly IGenericRepository<DAL.Client> _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IGenericRepository<DAL.Client> clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
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
    }
}
