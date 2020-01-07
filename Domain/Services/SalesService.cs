using System.Collections.Generic;
using AutoMapper;

namespace BLL.Servises
{
    public class SalesService
    {
        private readonly IMapper _mapper;

        public SalesService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<Sale> Get(IEnumerable<Sales> salesEntities)
        {
            return _mapper.Map<IEnumerable<Sale>>(salesEntities);
        }
    }
}
