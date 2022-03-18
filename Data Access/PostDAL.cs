
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace Data_Access
{
    public class PostDAL : MSSQLConnection
    {
        public void Upload(PostDTO post)
        {
            if (Exists(post.ID))
            {
                Update(post);
            } else
            {
                OpenConnection();

                string query = "insert into Post (category_id, user_id, title, creation_date) values" +
                    $"(1, 1, '{post.Name}', '{post.CreationDate.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}');";
                SqlCommand cmd = new SqlCommand(query, connection);
                Console.WriteLine(query);


                cmd.ExecuteNonQuery();

                CloseConnection();
            }
        }

        private void Update(PostDTO post)
        {
            
        }

        public List<PostDTO> LoadAll()
        {
            OpenConnection();

            string query = "SELECT * FROM Post";
            SqlCommand cmd = new SqlCommand(query, connection);


            List<PostDTO> result = new List<PostDTO>();
            int id = -1;
            string name = "";
            DateTime creationDate = new DateTime();
            int upvotes = -1;
            int categoryID = -1;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["id"]);
                    name = reader["title"].ToString();
                    creationDate = (DateTime)reader["creation_date"];
                    categoryID = Convert.ToInt32(reader["category_id"]);
                }
            }

            CategoryDTO category = LoadCategory(categoryID);
            List<CommentDTO> comments = GetComments(id);

            Dictionary<AttributeDTO, string> valuesByAttributes = new Dictionary<AttributeDTO, string>();
            foreach (AttributeDTO attribute in category.Attributes)
            {
                valuesByAttributes.Add(attribute, GetAttributeValue(id, attribute.ID));
            }

            PostDTO post = new PostDTO
            {
                ID = id,
                Name = name,
                CreationDate = creationDate,
                Comments = GetComments(id),
                Upvotes = upvotes,
                Category = category,
                ValuesByAttributes = valuesByAttributes
            };

            result.Add(post);
            
            CloseConnection();
            return result;
        }


        private List<CommentDTO> GetComments(int postID)
        {
            string query = $"SELECT * FROM Comment WHERE post_id = {postID}";
            SqlCommand cmd = new SqlCommand(query, connection);

            List<CommentDTO> result = new List<CommentDTO>();

            OpenConnection();
            int id = -1;
            string text = "";
            DateTime creationDate = new DateTime();
            int upvotes = -1;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = (int)reader["id"];
                    text = (string)reader["text"];
                    creationDate = (DateTime)reader["creation_date"];
                    upvotes = (int)reader["upvotes"];
                }
            }
            result.Add(new CommentDTO
            {
                ID = id,
                Text = text,
                CreationDate = creationDate,
                Upvotes = upvotes,
                Replies = GetCommentsFromComment(id)
            });
            CloseConnection();
            return result;
        }
        private List<CommentDTO> GetCommentsFromComment(int parentCommentID)
        {
            string query = $"SELECT * FROM Comment WHERE parent_comment_id = {parentCommentID}";
            OpenConnection();
            SqlCommand cmd = new SqlCommand(query, connection);

            List<CommentDTO> result = new List<CommentDTO>();

            int id = -1;
            string text = "";
            DateTime creationDate = new DateTime();
            int upvotes = -1;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = (int)reader["id"];
                    text = (string)reader["text"];
                    creationDate = (DateTime)reader["creation_date"];
                    upvotes = (int)reader["upvotes"];
                }
            }

            List<CommentDTO> replies = new List<CommentDTO>();
            if (id != -1)
            {
                replies = GetCommentsFromComment(id);
            }
            result.Add(new CommentDTO
            {
                ID = id,
                Text = text,
                CreationDate = creationDate,
                Upvotes = upvotes,
                Replies = replies
            });
            CloseConnection();
            return result;
        }


        private CategoryDTO LoadCategory(int id)
        {
            OpenConnection();

            string query = $"SELECT * FROM Category WHERE id = {id}";
            SqlCommand cmd = new SqlCommand(query, connection);
            Console.WriteLine("EXECUTING: " + query);
            CategoryDTO? result = null;

            string name = "";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    name = reader["name"].ToString();
                }
            }
                result = new CategoryDTO
                {
                    ID = id,
                    Name = name,
                    Attributes = LoadAttributes(id)
                };
            

            CloseConnection();

            if (result == null)
                Console.WriteLine($"Couldnt find category with ID {id}");
            return (CategoryDTO)result;
        }

        private string GetAttributeValue(int postID, int attributeID)
        {
            OpenConnection();

            string query = $"SELECT * FROM PostAttribute WHERE post_id = {postID} AND attribute_id = {attributeID}";
            SqlCommand cmd = new SqlCommand(query, connection);
            Console.WriteLine("EXECUTING: " + query);
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
        private protected List<AttributeDTO> LoadAttributes(int categoryID)
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

        private bool Exists(int postID)
        {
            connection.Open();

            string query = $"SELECT * FROM Post WHERE id = {postID}";
            SqlCommand cmd = new SqlCommand(query, connection);
            Console.WriteLine("EXECUTING: " + query);

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
