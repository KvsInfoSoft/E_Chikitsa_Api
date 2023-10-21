using E_Chikitsa_DBConfiguration.ConnectionServices;
using E_Chikitsa_Interfaces.InterfacesResources;
using E_Chikitsa_Repositories.RepositoriesResources;
using System.Runtime.CompilerServices;

namespace E_Chikitsa_Api.Configuration
{
    public static class ServicesConfiguration
    {

        public static void ConfigureRepositeries(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();           

        }

        public static void ConfigureServices( this IServiceCollection services)
        {            
            services.AddScoped<IADOConnectionRepository, AdoConnectionReositery>()
            .AddScoped<ILoginInterface, LoginServices>();
        }

    }
}
