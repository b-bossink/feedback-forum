using Interfaces;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Data_Access
{
    public class PostDAL : MSSQLConnection, IPostDAL
    {
        private readonly CategoryDAL _categoryDAL = new CategoryDAL();
        private readonly CommentDAL _commentDAL = new CommentDAL();
        private readonly MemberDAL _memberDAL = new MemberDAL();

        public int Upload(PostDTO post)
        {
            if (!OpenConnection())
                return 0;

            string query = "insert into Post (category_id, user_id, title, upvotes, creation_date) values" +
                $"(@CategoryID, @UserID, @Name, @Upvotes, @Date) SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.Add(new SqlParameter("@Name", post.Name));
            cmd.Parameters.Add(new SqlParameter("@CategoryID", post.Category.ID));
            cmd.Parameters.Add(new SqlParameter("@UserID", post.Owner.ID));
            cmd.Parameters.Add(new SqlParameter("@Upvotes", post.Upvotes));
            cmd.Parameters.Add(new SqlParameter("@Date", post.CreationDate.ToString("MM/dd/yyyy HH:mm:ss")));


            int savedRows = 0;
            int thisPostID = -1;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    thisPostID = Convert.ToInt32(reader.GetValue(0));
                    savedRows++;
                }
            }

            if (savedRows > 0) {
                foreach (KeyValuePair<AttributeDTO, string> valuesByAttribute in post.ValuesByAttributes)
                {
                    string attributeQuery = $"INSERT INTO PostAttribute (post_id, attribute_id, value) values (@PostID, @AttributeID, @Value)";

                    SqlCommand cmd2 = new SqlCommand(attributeQuery, connection);
                    cmd2.Parameters.Add(new SqlParameter("@PostID", thisPostID));
                    cmd2.Parameters.Add(new SqlParameter("@AttributeID", valuesByAttribute.Key.ID));
                    cmd2.Parameters.Add(new SqlParameter("@Value", valuesByAttribute.Value));
                    cmd2.ExecuteNonQuery();
                }
            }

            CloseConnection();
            return savedRows;
        }

        public List<PostDTO> LoadAll()
        {

            if (!OpenConnection())
                return null;

            string query = "SELECT * FROM Post";
            SqlCommand cmd = new SqlCommand(query, connection);
            List<PostDTO> result = new List<PostDTO>();


            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    Dictionary<AttributeDTO, string> valuesByAttributes = new Dictionary<AttributeDTO, string>();

                    CategoryDTO category = (CategoryDTO)_categoryDAL.Get((int)reader["category_id"]);
                    foreach (AttributeDTO attribute in category.Attributes)
                    {
                        valuesByAttributes.Add(attribute, GetAttributeValue(id, attribute.ID));
                    }

                    PostDTO post = new PostDTO
                    {
                        ID = id,
                        Name = (string)reader["title"],
                        CreationDate = (DateTime)reader["creation_date"],
                        Upvotes = (int)reader["upvotes"],
                        Category = category,
                        Comments = _commentDAL.GetFromPost(id),
                        ValuesByAttributes = valuesByAttributes,
                        Owner = (MemberDTO)_memberDAL.Get((int)reader["user_id"])
                    };

                    result.Add(post);
                }
            }

            CloseConnection();
            return result;
        }

        public int Delete(int id)
        {
            if (!OpenConnection())
                return 0;

            Dictionary<string, string> tableWithColumn = new Dictionary<string, string>()
            {
                { "Comment", "post_id"},
                { "PostAttribute", "post_id"},
                { "Post", "id"}
            };

            int result = 0;
            foreach (KeyValuePair<string,string> kvp in tableWithColumn)
            {
                string query = String.Format("DELETE FROM {0} WHERE {1} = @ID", kvp.Key, kvp.Value);
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                result = cmd.ExecuteNonQuery();
            }

            CloseConnection();
            return result;
        }

        public int Update(PostDTO post)
        {
            if (!OpenConnection())
                return 0;

            string query = $"UPDATE Post SET " +
                $"title = @Name, " +
                $"category_id = @CategoryID, " +
                $"user_id = @UserID, " +
                $"creation_date = @Date, " +
                $"upvotes = @Upvotes " +
                $"WHERE id = @ID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(new SqlParameter("@ID", post.ID));
            cmd.Parameters.Add(new SqlParameter("@Name", post.Name));
            cmd.Parameters.Add(new SqlParameter("@CategoryID", post.Category.ID));
            cmd.Parameters.Add(new SqlParameter("@UserID", post.Owner.ID));
            cmd.Parameters.Add(new SqlParameter("@Upvotes", post.Upvotes));
            cmd.Parameters.Add(new SqlParameter("@Date", post.CreationDate.ToString("MM/dd/yyyy HH:mm:ss")));
            int result = cmd.ExecuteNonQuery();

            foreach (KeyValuePair<AttributeDTO, string> valuesByAttribute in post.ValuesByAttributes)
            {
                string attributeQuery = $"INSERT INTO PostAttribute (post_id, attribute_id, value) values (@PostID, @AttributeID, @Value)";

                SqlCommand cmd2 = new SqlCommand(attributeQuery, connection);
                cmd2.Parameters.Add(new SqlParameter("@PostID", post.ID));
                cmd2.Parameters.Add(new SqlParameter("@AttributeID", valuesByAttribute.Key.ID));
                cmd2.Parameters.Add(new SqlParameter("@Value", valuesByAttribute.Value));
                cmd2.ExecuteNonQuery();
            }

            CloseConnection();

            return result;
        }

        private string GetAttributeValue(int postID, int attributeID)
        {
            if (!OpenConnection())
                return null;

            string query = $"SELECT * FROM PostAttribute WHERE post_id = @ID " +
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

            //CloseConnection();

            return result;
        }

        public bool Exists(int id)
        {
            string query = "SELECT COUNT (*) FROM Post WHERE id = @ID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(new SqlParameter("@ID", id));

            OpenConnection();
            int result = (int)cmd.ExecuteScalar();
            CloseConnection();
            return result > 0;
        }
    }
}
