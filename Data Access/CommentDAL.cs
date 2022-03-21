using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data_Access
{
    class CommentDAL : MSSQLConnection
    {
        public List<CommentDTO> GetFromPost(int postID)
        {
            string query = $"SELECT * FROM Comment WHERE post_id = {postID}";
            SqlCommand cmd = new SqlCommand(query, connection);

            List<CommentDTO> firstResult = new List<CommentDTO>();

            OpenConnection();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string text = (string)reader["text"];
                    DateTime creationDate = (DateTime)reader["creation_date"];
                    int upvotes = (int)reader["upvotes"];

                    firstResult.Add(new CommentDTO
                    {
                        ID = id,
                        Text = text,
                        CreationDate = creationDate,
                        Upvotes = upvotes
                    });
                }
            }
            CloseConnection();

            List<CommentDTO> finalResult = firstResult;
            for (int i = 0; i < firstResult.Count; i++)
            {
                finalResult[i] = new CommentDTO
                {
                    ID = firstResult[i].ID,
                    Text = firstResult[i].Text,
                    CreationDate = firstResult[i].CreationDate,
                    Upvotes = firstResult[i].Upvotes,
                    Replies = GetFromComment(firstResult[i].ID)
                };
            }

            return finalResult;
        }

        private List<CommentDTO> GetFromComment(int parentCommentID)
        {
            string query = $"SELECT * FROM Comment WHERE parent_comment_id = {parentCommentID}";
            OpenConnection();
            SqlCommand cmd = new SqlCommand(query, connection);

            List<CommentDTO> firstResult = new List<CommentDTO>();

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

                    firstResult.Add(new CommentDTO
                    {
                        ID = id,
                        Text = text,
                        CreationDate = creationDate,
                        Upvotes = upvotes
                    });
                }
            }

            CloseConnection();

            List<CommentDTO> finalResult = new List<CommentDTO>();
            for (int i = 0; i < firstResult.Count; i++)
            {
                List<CommentDTO> replies = new List<CommentDTO>();
                if (id != -1)
                {
                    replies = GetFromComment(id);
                }

                finalResult[i] = new CommentDTO
                {
                    ID = firstResult[i].ID,
                    Text = firstResult[i].Text,
                    CreationDate = firstResult[i].CreationDate,
                    Upvotes = firstResult[i].Upvotes,
                    Replies = replies
                };
            }


            return finalResult;
        }

    }
}
