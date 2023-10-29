using System.Runtime.InteropServices;
using static E_Chikitsa_DBConfiguration.DatabaseContext.DbInfo;

namespace E_Chikitsa_Api.Configuration
{
    public static class ConfigurationConnection
    {

        public static IServiceCollection AdoConnectionProvider(this IServiceCollection services,IConfiguration configuration)
        {
            //If so many connection needed just add same here.
            var connectionE_Chikitsa = string.Empty;
            var connectionE_Chikitsalocal = string.Empty;

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
               
                connectionE_Chikitsalocal = configuration.GetConnectionString("E_ChikitsaDbConnStrlocal");
                connectionE_Chikitsa = configuration.GetConnectionString("E_ChikitsaDbConnStr");
            }
            services.AddSingleton(new E_ChikitsaDbInfo(connectionE_Chikitsalocal));
            services.AddSingleton(new E_ChikitsaDbInfo(connectionE_Chikitsa));
            

            return services;
        }
    }
}
