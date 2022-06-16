using System.Data.SqlClient;

namespace Data_Access
{
    public abstract class MSSQLConnection
    {
        protected private SqlConnection connection =
            new SqlConnection("Server=mssqlstud.fhict.local;Database=dbi426602;User Id=dbi426602;Password=db80551Nk!;MultipleActiveResultSets=True");
    
        protected private bool OpenConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    return true;
                }
                return false;
            } catch (SqlException)
            {
                return false;
            }
        }

        protected private void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

    }
}
