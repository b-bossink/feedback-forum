using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FeedbackForum.Classes
{
    public class CategoryDatabase : MSSQLConnection
    {
        public List<Category> LoadAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "SELECT * FROM Category";
            SqlCommand cmd = new SqlCommand(query, conn);
            Console.WriteLine("EXECUTING: " + query);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Category category = new Category(reader["name"].ToString(), LoadAttributes(Convert.ToInt32(reader["id"])));
                }
            }
            return null;
        }
    }
}
