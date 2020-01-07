using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL;
using DAL.Repository;

namespace BLL.Services
{
    public class SaleService: IService<DAL.Sale, BLL.Sale>
    {
        private readonly IGenericRepository<DAL.Sale> _saleRepository;
        private readonly IMapper _mapper;

        public SaleService(IGenericRepository<DAL.Sale> saleRepository, IMapper mapper)
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
            throw new NotImplementedException();
        }

        public void Add(IEnumerable<DAL.Sale> Entities)
        {
            throw new NotImplementedException();
        }

        public void Remove(DAL.Sale Entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(IEnumerable<DAL.Sale> Entities)
        {
            throw new NotImplementedException();
        }
    }
}
