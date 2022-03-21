
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
        private List<AttributeDTO> LoadAttributes(int categoryID)
        {
            OpenConnection();

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

            CloseConnection();
            return result;
        }
        public CategoryDTO Load(int id)
        {
            OpenConnection();

            string query = $"SELECT * FROM Category WHERE id = {id}";
            SqlCommand cmd = new SqlCommand(query, connection);
            Console.WriteLine("EXECUTING: " + query);
            CategoryDTO firstResult = new CategoryDTO();

            string name = "";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    name = reader["name"].ToString();
                }

                firstResult = new CategoryDTO
                {
                    ID = id,
                    Name = name
                };
            }

            CloseConnection();

            return new CategoryDTO
            {
                ID = firstResult.ID,
                Name = firstResult.Name,
                Attributes = LoadAttributes(firstResult.ID)
            };
        }
    }
}
