using evoting.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace evoting.Models
{
    public class DataContext
    {
        public void CreateVote(House model)
        {
            SqlConnection conn = new SqlConnection("Data Source=NIRA; User ID=sa; Password=Khanalnira; Initial Catalog=Evoting;Trusted_Connection=True;TrustServerCertificate=True;");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"Insert into dbo.Vote(vusername,house,candidatetype,candidate)
                                            values(@Username,@house,@candidatetype,@candidate)";
            cmd.Parameters.Add(new SqlParameter("@Username", model.Username));
            cmd.Parameters.Add(new SqlParameter("@house", model.house));
            cmd.Parameters.Add(new SqlParameter("@candidatetype", model.candidatetype));
            cmd.Parameters.Add(new SqlParameter("@candidate", model.candidate));
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }
}



