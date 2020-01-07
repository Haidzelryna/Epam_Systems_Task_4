using AutoMapper;

namespace BLL.Mapper
{
    public static class Facade
    {
        public static void StartMapping(IMapperConfigurationExpression mapperCfg)
        {
            mapperCfg.CreateMap<BLL.Sales, BLL.Sale>();
            mapperCfg.CreateMap<BLL.Sale, DAL.Sale>();
            mapperCfg.CreateMap<BLL.Contact, DAL.Contact>();
        }
    }
}
