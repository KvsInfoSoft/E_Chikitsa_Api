using E_Chikitsa_DBConfiguration.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace E_Chikitsa_DBConfiguration.ConnectionServices
{
    public class AdoConnectionConfiguration
    {
        private readonly AdoContext _context;
        private int _commandTimeOut = 300;
        private readonly string _connectionString = string.Empty;
        public int CommandTimeOut
        {
            get
            {
                return _commandTimeOut;
            }
            set
            {
                _commandTimeOut = value;
            }
        }

        public AdoConnectionConfiguration(AdoContext context)
        {
            this._context = context;

        }
        public int ExecuteData(string spName, params SqlParameter[] sqlParams)
        {
            int rows = -1;
            SqlCommand cmd = null;
            try
            {
                using SqlConnection conn = _context.GetConnection();
                bool openConn = (conn.State == ConnectionState.Open);
                if (!openConn)
                {
                    conn.Open();
                }
                using (cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (sqlParams != null)
                    {
                        cmd.Parameters.AddRange(sqlParams);
                        rows = cmd.ExecuteNonQuery();
                    }
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                if (openConn)
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                cmd.Parameters.Clear();
                cmd.Dispose();
                rows = -3;
            }
            return rows;
        }
        public DataSet GetData(string spName, params SqlParameter[] sqlParams)
        {
            DataSet ds;
            try
            {
                SqlConnection connection = _context.GetConnection();
                SqlCommand cmd = new SqlCommand(spName, connection);
                cmd.CommandTimeout = _commandTimeOut;
                cmd.Parameters.AddRange(sqlParams);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ds = new DataSet();
                ad.Fill(ds);
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }
        public DataTable GetDataTable(string spName, params SqlParameter[] sqlParams)
        {
            SqlConnection connection = _context.GetConnection();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(spName, connection);
                cmd.CommandTimeout = _commandTimeOut;
                cmd.Parameters.AddRange(sqlParams);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return dt;
        }
        public DataTable GetDataById(string spName, params SqlParameter[] sqlParams)
        {
            try
            {
                var res = GetData(spName, sqlParams);
                return res?.Tables.Count > 0 ? res.Tables[0] : null;
            }
            catch
            {
                return null;
            }
        }
        public string GetSingleCell(string spName, params SqlParameter[] sqlParams)
        {
            string res;
            try
            {
                SqlConnection conn = _context.GetConnection();
                bool openConn = (conn.State == ConnectionState.Open);
                if (!openConn)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(spName, conn);
                cmd.CommandTimeout = _commandTimeOut;
                cmd.Parameters.AddRange(sqlParams);
                cmd.CommandType = CommandType.StoredProcedure;
                res = Convert.ToString(cmd.ExecuteScalar());
                cmd.Parameters.Clear();
                cmd.Dispose();
                if (openConn)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return res;
        }
        public ArrayList ExecuteDataWithOutPut(string spName, params SqlParameter[] sqlParams)
        {
            int rows = -1;
            SqlCommand cmd = null;
            ArrayList objArrayList = null;
            try
            {
                using SqlConnection conn = _context.GetConnection();
                bool openConn = (conn.State == ConnectionState.Open);
                if (!openConn)
                {
                    conn.Open();
                }
                using (cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = _commandTimeOut;
                    if (sqlParams != null)
                    {
                        cmd.Parameters.AddRange(sqlParams);
                        cmd.ExecuteNonQuery();
                    }
                    objArrayList = new ArrayList();
                    foreach (SqlParameter objSqlParameter in cmd.Parameters)
                    {
                        if (objSqlParameter.Direction == ParameterDirection.InputOutput || objSqlParameter.Direction == ParameterDirection.Output || objSqlParameter.Direction == ParameterDirection.ReturnValue)
                        {
                            objArrayList.Add(objSqlParameter.Value);
                        }
                    }
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                if (openConn)
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                rows = -1;
            }
            return objArrayList;
        }
        public async Task ExecuteStoreProcedureAsync(string storeProcedureName, SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }
        public async Task ExecuteStoreProcedureAsync(string storeProcedureName, SqlParameter[] parameters, string connectionString)
        {
            try
            {
                using SqlConnection cn = _context.GetConnection();
                await cn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (_commandTimeOut != -1) cmd.CommandTimeout = _commandTimeOut;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (parameters != null) foreach (SqlParameter sqlParameter in parameters) cmd.Parameters.Add(sqlParameter);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<T>> ExecuteStoreProcedureReturnListObjectAsync<T>(string storeProcedureName, SqlParameter[] parameters)
        {
            try
            {
                List<T> dataToReturn;
                using (SqlConnection cn = _context.GetConnection())
                {
                    await cn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storeProcedureName, cn))
                    {
                        if (_commandTimeOut != -1) cmd.CommandTimeout = _commandTimeOut;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if (parameters != null) foreach (SqlParameter sqlParameter in parameters) cmd.Parameters.Add(sqlParameter);
                        SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                        dataToReturn = await new GenericPopulator<T>().PopulateList(dataReader);
                    }
                }
                return dataToReturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<T> ExecuteStoreProcedureAndReturnObjectAsync<T>(string storeProcedureName, SqlParameter[] parameters)
        {
            try
            {
                T dataToReturn;
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    await cn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storeProcedureName, cn))
                    {
                        if (_commandTimeOut != -1) cmd.CommandTimeout = _commandTimeOut;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if (parameters != null) foreach (SqlParameter sqlParameter in parameters) cmd.Parameters.Add(sqlParameter);
                        SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                        dataToReturn = await new GenericPopulator<T>().Populate(dataReader);
                    }
                }
                return dataToReturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<DataTable> ExecuteStoreProcedureAndReturnDataTableAsync<T>(string storeProcedureName, SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
