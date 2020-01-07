using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Repository;

namespace BLL.Servises
{
    public class SalesService
    {
        //private readonly IGenericRepository<Sales> _salesRepository;
        private readonly IMapper _mapper;

        public SalesService(IMapper mapper)//(IGenericRepository<Sales> salesRepository, IMapper mapper)
        {
            //_salesRepository = salesRepository;
            _mapper = mapper;
        }

        public IEnumerable<Sale> Get(IEnumerable<Sales> salesEntities)
        {
            //Task<IEnumerable<Sales>> salesEntities = _salesRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Sale>>(salesEntities);
        }
    }
}
