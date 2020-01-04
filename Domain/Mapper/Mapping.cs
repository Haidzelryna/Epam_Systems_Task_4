using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Models;

namespace Domain.Mapper
{
    public static class Mapping
    {
        //private static readonly MapperConfiguration ADAPTERMAPPERCFG;

        private static Domain.Sale _domainSale = null;
        private static Models.Sale _modelsSale = null;

        public static void Map(Domain.Sale domainSale, Models.Sale modelsSale)
        {
            _domainSale = domainSale;
            _modelsSale = modelsSale;
            var ADAPTERMAPPERCFG = new MapperConfiguration(cfg => cfg.CreateMap<Domain.Sale, Models.Sale>());
            var mapper = ADAPTERMAPPERCFG.CreateMapper();
            mapper.Map(_domainSale, _modelsSale);
        }
    }
}
