using E_Chikitsa_DBConfiguration.ConnectionServices;
using E_Chikitsa_DBConfiguration.DatabaseContext;
using E_Chikitsa_Interfaces.InterfacesResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static E_Chikitsa_DBConfiguration.DatabaseContext.DbInfo;

namespace E_Chikitsa_Repositories.RepositoriesResources
{
    public class DoctorsRepositories :IDoctors
    {
        private readonly AdoConnectionReositery _adoDBContext;
        #region Constructor
        public DoctorsRepositories(E_ChikitsaDbInfo dbInfo)
        {
            _adoDBContext = new AdoConnectionReositery(new AdoContext(dbInfo.ConnectionString));

        } 
        #endregion
    }
}
