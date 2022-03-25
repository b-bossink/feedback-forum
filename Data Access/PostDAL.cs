
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace Data_Access
{
    public class PostDAL : MSSQLConnection
    {
        private CategoryDAL categoryDAL = new CategoryDAL();
        private CommentDAL commentDAL = new CommentDAL();

        public void Upload(PostDTO post)
        {
            if (Exists(post.ID))
            {
                Update(post); // tomorrow:)
            } else
            {
                OpenConnection();
                
                string query = "insert into Post (category_id, user_id, title, upvotes, creation_date) values" +
                    $"(1, 1, '{post.Name}', {post.Upvotes}, '{post.CreationDate.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}') SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, connection);
                
                int thisPostID = -1;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        thisPostID = Convert.ToInt32(reader.GetValue(0));
                    }
                }


                foreach (KeyValuePair<AttributeDTO,string> valuesByAttribute in post.ValuesByAttributes)
                {
                    string attributeQuery = $"INSERT INTO PostAttribute (post_id, attribute_id, value) values ({thisPostID}, '{valuesByAttribute.Key.ID}', '{valuesByAttribute.Value}')";
                    SqlCommand cmd2 = new SqlCommand(attributeQuery, connection);
                    cmd2.ExecuteNonQuery();
                }

                CloseConnection();
            }
        }

        public List<PostDTO> LoadAll()
        {
            OpenConnection();

            string query = "SELECT * FROM Post";
            SqlCommand cmd = new SqlCommand(query, connection);


            List<PostDTO> firstResult = new List<PostDTO>();
            int id = -1;
            List<int> categoryIDs = new List<int>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["id"]);
                    string name = reader["title"].ToString();
                    DateTime creationDate = (DateTime)reader["creation_date"];
                    categoryIDs.Add(Convert.ToInt32(reader["category_id"]));
                    int upvotes = Convert.ToInt32(reader["upvotes"]);

                    PostDTO post = new PostDTO
                    {
                        ID = id,
                        Name = name,
                        CreationDate = creationDate,
                        Upvotes = upvotes
                    };

                    firstResult.Add(post);
                }
            }

            CloseConnection();

            List<PostDTO> finalResult = firstResult;
            for (int i = 0; i < firstResult.Count; i++)
            {
                
                CategoryDTO category = categoryDAL.Load(categoryIDs[i]);

                Dictionary<AttributeDTO,string> valuesByAttributes = new Dictionary<AttributeDTO, string>();
                foreach (AttributeDTO attribute in category.Attributes)
                {
                    valuesByAttributes.Add(attribute, GetAttributeValue(firstResult[i].ID, attribute.ID));
                }

                finalResult[i] = new PostDTO {
                    ID = firstResult[i].ID,
                    Name = firstResult[i].Name,
                    Upvotes = firstResult[i].Upvotes,
                    CreationDate = firstResult[i].CreationDate,
                    Comments = commentDAL.GetFromPost(firstResult[i].ID),
                    Category = category,
                    ValuesByAttributes = valuesByAttributes
                };

            }
            return finalResult;
        }

        private void Update(PostDTO post)
        {

        }

        private string GetAttributeValue(int postID, int attributeID)
        {
            OpenConnection();

            string query = $"SELECT * FROM PostAttribute WHERE post_id = {postID} AND attribute_id = {attributeID}";
            SqlCommand cmd = new SqlCommand(query, connection);
            string result = "";

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result = reader["value"].ToString();
                }
            }

            CloseConnection();

            return result;
        }
        
        private bool Exists(int postID)
        {
            connection.Open();

            string query = $"SELECT * FROM Post WHERE id = {postID}";
            SqlCommand cmd = new SqlCommand(query, connection);

            bool result = false;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //to-do : make this work
                }
            }
            connection.Close();
            return result;
        }
    }
}
