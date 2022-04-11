using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using Interfaces;

namespace Data_Access
{
    public class CommentDAL : MSSQLConnection, ICommentDAL
    {
        public int Upload(CommentDTO comment, int parentPostID, int? parentCommentID = null)
        {
            object nullableParentCommentID = parentCommentID;
            if (parentCommentID == null)
            {
                nullableParentCommentID = Convert.DBNull;
            }

            OpenConnection();
            string query = "insert into Comment (post_id,user_id,text,upvotes,creation_date, parent_comment_id) values" +
                 $"(@PostID, @UserID, @Text, @Upvotes, @CreationDate, @ParentCommentID)";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.Add(new SqlParameter("@PostID", parentPostID));
            cmd.Parameters.Add(new SqlParameter("@UserID", 1));
            cmd.Parameters.Add(new SqlParameter("@Text", comment.Text));
            cmd.Parameters.Add(new SqlParameter("@Upvotes", comment.Upvotes));
            cmd.Parameters.Add(new SqlParameter("@CreationDate",
                comment.CreationDate.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
            cmd.Parameters.Add(new SqlParameter("@ParentCommentID", nullableParentCommentID));

            int savedRows = cmd.ExecuteNonQuery();
            return savedRows;
        }

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

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(CommentDTO comment)
        {
            throw new NotImplementedException();
        }
    }
}
