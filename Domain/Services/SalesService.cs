using System.Collections.Generic;
using AutoMapper;

namespace BLL.Services
{
    public class SalesService: IService<BLL.Sale, BLL.Sales>
    {
        private readonly IMapper _mapper;

        public SalesService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Sale Get(Sales Entity)
        {
            return _mapper.Map<BLL.Sale>(Entity);
        }

        public IEnumerable<BLL.Sale> Get(IEnumerable<BLL.Sales> Entities)
        {
            return _mapper.Map<IEnumerable<BLL.Sale>>(Entities);
        }

        public void Add(Sale Entity)
        {
            throw new System.NotImplementedException();
        }

        public void Add(IEnumerable<Sale> Entities)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Sale Entity)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(IEnumerable<Sale> Entities)
        {
            throw new System.NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}
