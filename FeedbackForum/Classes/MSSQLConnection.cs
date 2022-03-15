using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace FeedbackForum.Classes
{
    public class MSSQLConnection
    {
        protected private string connectionString = "Server=mssqlstud.fhict.local;Database=dbi426602;User Id=dbi426602;Password=db80551Nk!;";

        private protected List<Attribute> LoadAttributes(int categoryID)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = $"SELECT * FROM Attribute WHERE category_id = {categoryID}";
            SqlCommand cmd = new SqlCommand(query, conn);
            Console.WriteLine("EXECUTING: " + query);
            List<Attribute> result = new List<Attribute>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Attribute(reader["name"].ToString(), Convert.ToInt32(reader["id"])));
                }
            }

            conn.Close();
            return result;
        }




    }
}
