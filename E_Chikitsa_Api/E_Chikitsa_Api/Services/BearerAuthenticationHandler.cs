using E_Chikitsa_Interfaces.InterfacesResources;
using E_Chikitsa_Utility.UtilityTools.Constrains;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace E_Chikitsa_Api.Services
{
    public class BearerAuthenticationHandler: AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IAuthenticationServices _basicAuthentication;

        public BearerAuthenticationHandler(IMemoryCache memoryCache, IAuthenticationServices basicAuthentication, IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _memoryCache = memoryCache;
            _basicAuthentication = basicAuthentication;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                //ToDO: write authentication related logic.

                return  AuthenticateResult.NoResult();
            }
            catch (Exception ex)
            {

                return AuthenticateResult.Fail(ex.Message);
            }
        }

    }
}
