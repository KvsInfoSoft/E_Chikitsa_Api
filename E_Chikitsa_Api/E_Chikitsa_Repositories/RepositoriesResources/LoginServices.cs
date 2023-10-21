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
        private readonly AdoConnectionReositery _adoDBContext;
        public LoginServices(E_ChikitsaDbInfo dbInfo)
        {
            _adoDBContext = new AdoConnectionReositery(new AdoContext(dbInfo.ConnectionString));

        }

        public  async Task<UserLoginDetailModel> GetUsersLogin(UserLoginModel userLoginModel)
        {
            SqlParameter[] p =
            {
                new SqlParameter(SqlParameterConstrains.TRANSTYPE,"I"),
                new SqlParameter(SqlParameterConstrains.VNO,2),
                new SqlParameter(SqlParameterConstrains.USERNAME,"abc"),
                new SqlParameter(SqlParameterConstrains.PASSWORD ,"abc"),
                new SqlParameter(SqlParameterConstrains.ISDOC,2),
                new SqlParameter(SqlParameterConstrains.DOCID,5),
                new SqlParameter(SqlParameterConstrains.ENABLEDUPPRINT,5),
                new SqlParameter(SqlParameterConstrains.ENABLESHIFT,5),
                new SqlParameter(SqlParameterConstrains.USERDEPT,"I"),
                new SqlParameter(SqlParameterConstrains.USERPERMISSION,"s"),
                new SqlParameter(SqlParameterConstrains.ISDEACTIVE,1),
                new SqlParameter(SqlParameterConstrains.UNAME,"I"),
                new SqlParameter(SqlParameterConstrains.COMPUNAME,"I"),
                new SqlParameter(SqlParameterConstrains.PERDETAIL,"I"),
                new SqlParameter(SqlParameterConstrains.USRPRDCATFLAG,1),
                new SqlParameter(SqlParameterConstrains.USRPRDCATID,1),
                new SqlParameter(SqlParameterConstrains.ISWARD,1),
                new SqlParameter(SqlParameterConstrains.WARDID,1)
            };

            var ds =  _adoDBContext.GetDataTable(StoreProcedureConstarins.SPMANAGEUSER, p);
            var res = ds?.ConvertDataTable<UserLoginDetailModel>();
            return res.FirstOrDefault();
        }
    }
}
