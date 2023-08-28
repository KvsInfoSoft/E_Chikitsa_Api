using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Chikitsa_DBConfiguration.DatabaseContext
{
    public class AdoContext
    {
        public string ConnectionString { get; set; }

        public AdoContext(string connectionString)
        {
          this.ConnectionString = connectionString;
        }
        /// <summary>
        /// Globally connection get
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
