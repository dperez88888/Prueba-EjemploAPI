using Microsoft.Extensions.DependencyInjection;
using WatchDog;

namespace PruebaEjemploAPI.Persistence.Extensions.WatchDogX
{
    public static class WatchDogExtensions
    {
        public static IServiceCollection AddWatchDog(this IServiceCollection services, string connectionString)
        {
            services.AddWatchDogServices(c =>
            {
                c.SetExternalDbConnString = connectionString;
                c.DbDriverOption = WatchDog.src.Enums.WatchDogDbDriverEnum.MSSQL;
                c.IsAutoClear = true;
                c.ClearTimeSchedule = WatchDog.src.Enums.WatchDogAutoClearScheduleEnum.Weekly;
            });

            return services;
        }
    }
}
