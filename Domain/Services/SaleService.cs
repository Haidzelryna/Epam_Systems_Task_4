using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Repository;

namespace BLL.Servises
{
    public class SaleService
    {
        private readonly IGenericRepository<DAL.Sale> _saleRepository;
        private readonly IMapper _mapper;

        public SaleService(IGenericRepository<DAL.Sale> saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public IEnumerable<DAL.Sale> Get(IEnumerable<BLL.Sale> saleEntities)
        {
            //Task<IEnumerable<Sale>> saleEntities = _saleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DAL.Sale>>(saleEntities);
        }
    }
}
