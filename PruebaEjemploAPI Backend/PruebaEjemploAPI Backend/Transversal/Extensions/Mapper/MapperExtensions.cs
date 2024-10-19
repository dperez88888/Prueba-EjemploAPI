using AutoMapper;
using PruebaEjemploAPI_Backend.Transversal.Mapper;

namespace PruebaEjemploAPI_Backend.Transversal.Extensions.Mapper
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var mapperConf = new MapperConfiguration(conf =>
            {
                var profile = new ClienteEntityMapperProfile();
                conf.AllowNullCollections = true;
                conf.AddGlobalIgnore("Item");
                conf.AddProfile(profile);
            });

            var mapper = mapperConf.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
