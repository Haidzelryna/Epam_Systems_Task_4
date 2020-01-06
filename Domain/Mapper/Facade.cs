using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace BLL.Mapper
{
    public static class Facade
    {
        public static void StartMapping(IMapperConfigurationExpression mapperCfg)
        {
            mapperCfg.CreateMap<BLL.Sales, BLL.Sale>();
            mapperCfg.CreateMap<BLL.Sale, DAL.Sale>();
        }

        //public static void Map(IEnumerable<BLL.Sales> domainSales, IEnumerable<BLL.Sale> domainSale)
        //{
        //    _sales = domainSales;
        //    _domainSale = domainSale;
        //    AdapterMapperCfg = new MapperConfiguration(cfg => cfg.CreateMap<BLL.Sales, BLL.Sale>());
        //    var mapper = AdapterMapperCfg.CreateMapper();
        //    mapper.Map(_sales, _domainSale);
        //}

        //public static void Map(IEnumerable<BLL.Sale> domainSale, IEnumerable<DAL.Sale> modelsSale)
        //{
        //    _domainSale = domainSale;
        //    _modelsSale = modelsSale;
        //    AdapterMapperCfg = new MapperConfiguration(cfg => cfg.CreateMap<BLL.Sale, DAL.Sale>());
        //    var mapper = AdapterMapperCfg.CreateMapper();
        //    mapper.Map(_domainSale, _modelsSale);
        //}
    }
}
