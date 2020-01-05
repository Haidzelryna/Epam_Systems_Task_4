using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Domain.Mapper
{
    public static class Mapping
    {
        private static MapperConfiguration AdapterMapperCfg;

        private static IEnumerable<Domain.Sales> _sales = null;
        private static IEnumerable<Domain.Sale> _domainSale = null;
        private static IEnumerable<Models.Sale> _modelsSale = null;

        public static void Map(IEnumerable<Domain.Sales> domainSales, IEnumerable<Domain.Sale> domainSale)
        {
            _sales = domainSales;
            _domainSale = domainSale;
            AdapterMapperCfg = new MapperConfiguration(cfg => cfg.CreateMap<Domain.Sales, Domain.Sale>());
            var mapper = AdapterMapperCfg.CreateMapper();
            mapper.Map(_sales, _domainSale);
        }

        public static void Map(IEnumerable<Domain.Sale> domainSale, IEnumerable<Models.Sale> modelsSale)
        {
            _domainSale = domainSale;
            _modelsSale = modelsSale;
            AdapterMapperCfg = new MapperConfiguration(cfg => cfg.CreateMap<Domain.Sale, Models.Sale>());
            var mapper = AdapterMapperCfg.CreateMapper();
            mapper.Map(_domainSale, _modelsSale);
        }
    }
}
