using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Repository;

namespace BLL.Servises
{
    public class SaleService
    {
        private readonly IGenericRepository<Sale> _saleRepository;
        private readonly IMapper _mapper;

        public SaleService(IGenericRepository<Sale> saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public IEnumerable<Sale> Get()
        {
            Task<IEnumerable<Sale>> saleEntities = _saleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Sale>>(saleEntities);
        }
    }
}
