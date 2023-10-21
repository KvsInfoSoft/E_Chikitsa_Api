using E_Chikitsa_ViewModels.RequestModel;
using E_Chikitsa_ViewModels.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Chikitsa_Interfaces.InterfacesResources
{
    public interface ILoginInterface
    {
        Task<UserLoginDetailModel> GetUsersLogin(UserLoginModel userLoginModel);
    }
}
