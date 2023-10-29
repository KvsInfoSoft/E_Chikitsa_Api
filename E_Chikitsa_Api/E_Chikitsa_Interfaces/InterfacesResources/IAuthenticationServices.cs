using E_Chikitsa_ViewModels.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Chikitsa_Interfaces.InterfacesResources
{
    public interface IAuthenticationServices
    {

        Task<(bool isValid, LoginServiceModel loginInfo)> ValidateUserToken(string token, string password);
    }
}
