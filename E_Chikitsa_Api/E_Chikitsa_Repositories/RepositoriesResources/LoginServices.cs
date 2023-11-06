using E_Chikitsa_DBConfiguration.ConnectionServices;
using E_Chikitsa_DBConfiguration.DatabaseContext;
using E_Chikitsa_Interfaces.InterfacesResources;
using E_Chikitsa_Utility.UtilityTools;
using E_Chikitsa_Utility.UtilityTools.Constrains;
using E_Chikitsa_ViewModels.RequestModel;
using E_Chikitsa_ViewModels.ResponseModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static E_Chikitsa_DBConfiguration.DatabaseContext.DbInfo;

namespace E_Chikitsa_Repositories.RepositoriesResources
{
    public class LoginServices : ILoginInterface
    {
        #region Varibles
        private readonly AdoConnectionReositery _adoDBContext;
        #endregion

        #region Constructor
        public LoginServices(E_ChikitsaDbInfo dbInfo)
        {
            _adoDBContext = new AdoConnectionReositery(new AdoContext(dbInfo.ConnectionString));

        }
        #endregion

        #region GetUsersLogin
        public async Task<UserLoginDetailModel> GetUsersLogin(UserLoginModel userLoginModel)
        {
            SqlParameter[] p =
            {
                new SqlParameter(SqlParameterConstrains.USERNAME,userLoginModel.UserName)
            };
            var ds = _adoDBContext.GetDataTable(StoreProcedureConstarins.SPGETUSERDETAILONLINE, p);
            if (ds == null) return null;
            var res = ds.ConvertDataTable<UserLoginDetailModel>();
            return res.FirstOrDefault();
        } 
        #endregion
    }
}
