using AutoMapper;

namespace BLL.Mapper
{
    public class SetupMapping
    {
        public static IMapper SetupMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                Facade.StartMapping(cfg);

                cfg.CreateMap<BLL.Sales, BLL.Sale>();
                cfg.CreateMap<BLL.Sale, DAL.Sale>();

                //cfg.CreateMap<BLL.Sales, BLL.Sale>();
                //cfg.CreateMap<BLL.Sale, DAL.Sale>();
            });

            //return new Mapper(config);
            return config.CreateMapper();
        }
    }
}
