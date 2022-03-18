
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data_Access
{
    public class CategoryDAL : MSSQLConnection
    {
        public List<CategoryDTO> LoadAll()
        {
            connection.Open();

            string query = "SELECT * FROM Category";
            SqlCommand cmd = new SqlCommand(query, connection);
            Console.WriteLine("EXECUTING: " + query);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    CategoryDTO category = new CategoryDTO {
                        Name = reader["name"].ToString(),
                        Attributes = LoadAttributes(Convert.ToInt32(reader["id"])) };
                }
            }
            return null;
        }

        private protected List<AttributeDTO> LoadAttributes(int categoryID)
        {
            connection.Open();

            string query = $"SELECT * FROM Attribute WHERE category_id = {categoryID}";
            SqlCommand cmd = new SqlCommand(query, connection);
            Console.WriteLine("EXECUTING: " + query);
            List<AttributeDTO> result = new List<AttributeDTO>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new AttributeDTO
                    {
                        ID = Convert.ToInt32(reader["id"]),
                        Name = reader["name"].ToString()
                    });
                }
            }

            connection.Close();
            return result;
        }
    }
}
