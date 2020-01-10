using System.Collections.Generic;
using AutoMapper;
using DAL.Repository;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SaleService: IService<DAL.Sale, BLL.Sale>
    {
        private readonly IGenericRepository<DAL.Sale> _saleRepository;
        private readonly IMapper _mapper;

        public SaleService(IMapper mapper, IGenericRepository<DAL.Sale> saleRepository)
        {
            _saleRepository = saleRepository; 
            _mapper = mapper;
        }

        public DAL.Sale Get(Sale Entity)
        {
            return _mapper.Map<DAL.Sale>(Entity);
        }

        public IEnumerable<DAL.Sale> Get(IEnumerable<BLL.Sale> Entities)
        {
            return _mapper.Map<IEnumerable<DAL.Sale>>(Entities);
        }

        public void Add(DAL.Sale Entity)
        {
            _saleRepository.Add(Entity);
        }

        public void Add(IEnumerable<DAL.Sale> Entities)
        {
            _saleRepository.Add(Entities);
        }

        public void Remove(DAL.Sale Entity)
        {
            _saleRepository.Delete(Entity);
        }

        public void Remove(IEnumerable<DAL.Sale> Entities)
        {
            _saleRepository.Delete(Entities);
        }

        public void SaveChanges()
        {
            var banner = new Task(() => _saleRepository.SaveChanges());
            Task.WaitAll();
            Task.WhenAll(banner);

            //_saleRepository.SaveChanges();
        }
    }
}
