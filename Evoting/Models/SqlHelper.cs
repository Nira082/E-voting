using Microsoft.Data.SqlClient;
using System.Data;

namespace evoting.Models
{
    public class SqlHelper : IDisposable
    {
        private readonly IConfiguration _configuration;
        private SqlConnection _conn;
        public SqlHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _conn = new SqlConnection(_configuration["DbConnection"]);
        }

        public DataSet ExecuteDataset(string sql, List<SqlParameter> p, CommandType c) 
        {
            using (SqlCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = sql;
                if (p != null && p.Any()) 
                {
                    cmd.Parameters.AddRange(p.ToArray());
                }
                cmd.CommandType = c;

                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                return ds;
            }
        }

        public void ExecuteNonQuery(string sql, List<SqlParameter> p, CommandType c)
        {
            using (SqlCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = sql;
                if (p != null && p.Any())
                {
                    cmd.Parameters.AddRange(p.ToArray());
                }
                cmd.CommandType = c;

                if (_conn.State != ConnectionState.Open)
                    _conn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            if (_conn != null && _conn.State == System.Data.ConnectionState.Open) 
            {
                _conn.Close();
            }
            _conn?.Dispose();
        }
    }
}
