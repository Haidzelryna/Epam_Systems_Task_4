using System.Collections.Generic;
using AutoMapper;
using DAL;
using DAL.Repository;

namespace BLL.Services
{
    public class ContactService: IService<DAL.Contact, BLL.Contact>
    {
        private readonly IGenericRepository<DAL.Contact> _contactRepository;
        private readonly IMapper _mapper;

        public ContactService(IMapper mapper)
        {
            _contactRepository = new GenericRepository<DAL.Contact>();
            _mapper = mapper;
        }

        public DAL.Contact Get(BLL.Contact Entity)
        {
            return _mapper.Map<DAL.Contact>(Entity);
        }

        public IEnumerable<DAL.Contact> Get(IEnumerable<BLL.Contact> Entities)
        {
            return _mapper.Map<IEnumerable<DAL.Contact>>(Entities);
        }

        public void Remove(DAL.Contact Entity)
        {
            _contactRepository.Delete(Entity);
        }

        public void Remove(IEnumerable<DAL.Contact> Entities)
        {
            _contactRepository.Delete(Entities);
        }

        public void Add(DAL.Contact Entity)
        {
            _contactRepository.Add(Entity);
        }

        public void Add(IEnumerable<DAL.Contact> Entities)
        {
            _contactRepository.Add(Entities);
        }

        public void SaveChanges()
        {
            _contactRepository.SaveChanges();
        }
    }
}
