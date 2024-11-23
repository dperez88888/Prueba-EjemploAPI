using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PruebaEjemploAPI.Application.Common.Mapper;

namespace PruebaEjemploAPI.Application.Extensions.Mapper
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var mapperConf = new MapperConfiguration(conf =>
            {
                var profile = new EntityMapperProfile();
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
