using Interfaces;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data_Access
{
    public class CategoryDAL : MSSQLConnection, ICategoryDAL
    {
        public List<CategoryDTO> LoadAll()
        {
            if (!OpenConnection())
                return null;

            string query = "SELECT * FROM Category";
            SqlCommand cmd = new SqlCommand(query, connection);
            List<CategoryDTO> result = new List<CategoryDTO>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    result.Add(new CategoryDTO
                    {
                        ID = id,
                        Name = (string)reader["name"],
                        Attributes = LoadAttributes(id)
                    });
                }
            }
            CloseConnection();
            return result; 
        }

        public bool Upload(CategoryDTO category)
        {
            if (!OpenConnection())
                return false;

            bool saved;
            string query = "insert into Category (name) values" +
                $"('{category.Name}') SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, connection);

            int thisCategoryID = -1;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    thisCategoryID = Convert.ToInt32(reader.GetValue(0));
                }
                saved = reader.HasRows;
            }


            foreach (AttributeDTO attribute in category.Attributes)
            {
                string attributeQuery = $"INSERT INTO Attribute (category_id, name) values ({thisCategoryID}, '{attribute.Name}');";
                SqlCommand cmd2 = new SqlCommand(attributeQuery, connection);
                cmd2.ExecuteNonQuery();
            }

            CloseConnection();
            return saved;
        }

        public CategoryDTO Load(int id)
        {
            if (!OpenConnection())
                return new CategoryDTO { ID = 0 };

            string query = $"SELECT * FROM Category WHERE id = {id}";
            SqlCommand cmd = new SqlCommand(query, connection);
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

            //CloseConnection();

            return new CategoryDTO
            {
                ID = firstResult.ID,
                Name = firstResult.Name,
                Attributes = LoadAttributes(firstResult.ID)
            };
        }

        private List<AttributeDTO> LoadAttributes(int categoryID)
        {
            if (!OpenConnection())
                return null;

            string query = $"SELECT * FROM Attribute WHERE category_id = {categoryID}";
            SqlCommand cmd = new SqlCommand(query, connection);
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

            //CloseConnection();
            return result;
        }
    }
}
