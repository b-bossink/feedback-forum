using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using Interfaces;
using Interfaces.DTOs;

namespace Data_Access
{
    public class CommentDAL : MSSQLConnection, ICommentDAL
    {
        private readonly MemberDAL _memberDAL = new MemberDAL();
        public int Upload(CommentDTO comment, int postID)
        {
            if (!OpenConnection())
                return 0;

            string query = "insert into Comment (post_id,user_id,text,upvotes,creation_date) values" +
                 $"(@PostID, @UserID, @Text, @Upvotes, @CreationDate)";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.Add(new SqlParameter("@PostID", postID));
            cmd.Parameters.Add(new SqlParameter("@UserID", comment.Owner.ID));
            cmd.Parameters.Add(new SqlParameter("@Text", comment.Text));
            cmd.Parameters.Add(new SqlParameter("@Upvotes", comment.Upvotes));
            cmd.Parameters.Add(new SqlParameter("@CreationDate", comment.CreationDate.ToString("MM/dd/yyyy HH:mm:ss")));

            int savedRows = cmd.ExecuteNonQuery();
            return savedRows;
        }


        public int Upload(CommentDTO comment, int postID, int parentCommentID)
        {
            if (!OpenConnection())
                return 0;

            string query = "insert into Comment (post_id,user_id,text,upvotes,creation_date, parent_comment_id) values" +
                 $"(@PostID, @UserID, @Text, @Upvotes, @CreationDate, @ParentCommentID)";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.Add(new SqlParameter("@PostID", postID));
            cmd.Parameters.Add(new SqlParameter("@UserID", 1));
            cmd.Parameters.Add(new SqlParameter("@Text", comment.Text));
            cmd.Parameters.Add(new SqlParameter("@Upvotes", comment.Upvotes));
            cmd.Parameters.Add(new SqlParameter("@CreationDate", comment.CreationDate.ToString("MM/dd/yyyy HH:mm:ss")));
            cmd.Parameters.Add(new SqlParameter("@ParentCommentID", parentCommentID));

            int savedRows = cmd.ExecuteNonQuery();
            return savedRows;
        }

        public List<CommentDTO> GetFromPost(int postID)
        {
            string query = $"SELECT * FROM Comment WHERE post_id = {postID}";
            SqlCommand cmd = new SqlCommand(query, connection);

            List<CommentDTO> result = new List<CommentDTO>();

            if (!OpenConnection())
                return null;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    List<CommentDTO> replies = new List<CommentDTO>();
                    int id = (int)reader["id"];
                    result.Add(new CommentDTO
                    {
                        ID = id,
                        Text = (string)reader["text"],
                        CreationDate = (DateTime)reader["creation_date"],
                        Upvotes = (int)reader["upvotes"],
                        Replies = replies,
                        Owner = _memberDAL.Get((int)reader["user_id"])
                    });
                }
            }

            CloseConnection();
            return result;
        }

        public List<CommentDTO> GetFromComment(int parentCommentID)
        {
            string query = $"SELECT * FROM Comment WHERE parent_comment_id = {parentCommentID}";


            if (!OpenConnection())
                return null;

            SqlCommand cmd = new SqlCommand(query, connection);

            List<CommentDTO> result = new List<CommentDTO>();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    result.Add(new CommentDTO
                    {
                        ID = id,
                        Text = (string)reader["text"],
                        CreationDate = (DateTime)reader["creation_date"],
                        Upvotes = (int)reader["upvotes"],
                        Replies = GetFromComment(id),
                        Owner = _memberDAL.Get((int)reader["user_id"])
                    });
                }
            }

            CloseConnection();
            return result;
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
