using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Globalization;

namespace Data_Access
{
    public class MSSQLConnection
    {
        protected private SqlConnection connection =
            new SqlConnection("Server=mssqlstud.fhict.local;Database=dbi426602;User Id=dbi426602;Password=db80551Nk!;");

    
        protected private void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        protected private void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
    }
}
