using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Chikitsa_DBConfiguration.DatabaseContext
{
    public class DbInfo
    {
        /// <summary>
        /// add multiple db connection as per your requirent.
        /// </summary>
        public class E_ChikitsaDbInfo
        {
            public string ConnectionString { get; }
            public E_ChikitsaDbInfo (string connectionString) => ConnectionString = connectionString;   
        }
    }
}
