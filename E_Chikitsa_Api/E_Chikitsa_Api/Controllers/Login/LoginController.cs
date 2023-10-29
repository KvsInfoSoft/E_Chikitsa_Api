using E_Chikitsa_Interfaces.InterfacesResources;
using E_Chikitsa_Utility.UtilityTools.APIResponse;
using E_Chikitsa_Utility.UtilityTools.Constrains;
using E_Chikitsa_ViewModels.RequestModel;
using E_Chikitsa_ViewModels.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_Chikitsa_Api.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        #region Constructor
        private readonly ILoginInterface _loginInterface;

        public LoginController(ILoginInterface loginInterface)
        {
            _loginInterface = loginInterface;
        }
        #endregion

        [HttpPost("UserLogin")]

        public async Task<IActionResult> GetUserLogin(UserLoginModel userLogin)
        {
            SingleResponse<UserLoginDetailModel> userLoginDetailModel = new SingleResponse<UserLoginDetailModel>();

            if (ModelState.IsValid)
            {
                var res = await _loginInterface.GetUsersLogin(userLogin);

                if (res != null)
                {
                    userLoginDetailModel.Result = ResponseConstrains.RESULT_SUCCESS;
                    userLoginDetailModel.Message = ResponseConstrains.MSG_SUCCESS;
                    userLoginDetailModel.Data = res;
                    userLoginDetailModel.StatusCode = (int)HttpStatusCode.OK;
                }

            }

            return Ok(userLoginDetailModel);
        }


    }
}
