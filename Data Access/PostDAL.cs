
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

        public int Upload(PostDTO post)
        {
            int updated = 0;
            if (Exists(post.ID))
            {
                updated = Update(post);
            } else
            {
                OpenConnection();
                
                string query = "insert into Post (category_id, user_id, title, upvotes, creation_date) values" +
                    $"(@CategoryID, @UserID, '@Name', @Upvotes, '@Date') SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.Add(new SqlParameter("@Name", post.Name));
                cmd.Parameters.Add(new SqlParameter("@CategoryID", post.Category.ID));
                cmd.Parameters.Add(new SqlParameter("@UserID", 1));
                cmd.Parameters.Add(new SqlParameter("@Upvotes", post.Upvotes));
                cmd.Parameters.Add(new SqlParameter("@Date",
                    post.CreationDate.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));

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
                return thisPostID;
            }

            return updated;
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

        public int Delete(int id)
        {
            OpenConnection();

            string query = $"DELETE FROM PostAttribute WHERE post_id = @ID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.ExecuteNonQuery();

            query = $"DELETE FROM Post WHERE id = {id}";
            SqlCommand cmd2 = new SqlCommand(query, connection);
            cmd2.Parameters.Add(new SqlParameter("@ID", id));
            int result = cmd2.ExecuteNonQuery();

            CloseConnection();
            return result;
        }

        private int Update(PostDTO post)
        {
            OpenConnection();
            string query = $"UPDATE table_name SET " +
                $"title = @Name, " +
                $"category_id = @CategoryID, " +
                $"user_id = @UserID" +
                $"creation_date = @Date" +
                $"upvotes = @Upvotes" +
                $"WHERE id = @ID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(new SqlParameter("@ID", post.ID));
            cmd.Parameters.Add(new SqlParameter("@Name", post.Name));
            cmd.Parameters.Add(new SqlParameter("@CategoryID", post.Category.ID));
            cmd.Parameters.Add(new SqlParameter("@UserID", 1));
            cmd.Parameters.Add(new SqlParameter("@Upvotes", post.Upvotes));
            cmd.Parameters.Add(new SqlParameter("@Date",
                post.CreationDate.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        private string GetAttributeValue(int postID, int attributeID)
        {
            OpenConnection();

            string query = $"SELECT * FROM PostAttribute WHERE post_id = @ID" +
                "AND attribute_id = @AttributeID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(new SqlParameter("@ID", postID));
            cmd.Parameters.Add(new SqlParameter("@AttributeID", attributeID));
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
            OpenConnection();

            string query = $"SELECT * FROM Post WHERE id = {postID}";
            SqlCommand cmd = new SqlCommand(query, connection);

            bool result = false;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result = reader.HasRows;
                }
            }
            CloseConnection();
            return result;
        }
    }
}
