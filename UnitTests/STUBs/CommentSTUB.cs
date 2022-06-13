using System;
using System.Collections.Generic;
using Interfaces;
using Interfaces.DTOs;

namespace UnitTest.STUBs
{
    public class CommentSTUB : ICommentDAL
	{
        public PostSTUB postStub = new PostSTUB();

        public int Upload(CommentDTO comment, int postID)
        {
            List<PostDTO> database = postStub.LoadAll();
            foreach (PostDTO post in database)
            {
                if (post.ID == postID)
                {
                    int before = post.Comments.Count;
                    post.Comments.Add(comment);
                    int after = post.Comments.Count;
                    return after - before;
                }
            }
            return 404;
        }

        public int Upload(CommentDTO comment, int postID, int parentCommentID)
        {
            List<PostDTO> database = new PostSTUB().LoadAll();
            foreach (PostDTO post in database)
            {
                if (post.ID == postID)
                {
                    foreach (CommentDTO commentDTO in post.Comments)
                    {
                        if (commentDTO.ID == parentCommentID)
                        {
                            int before = commentDTO.Replies.Count;
                            commentDTO.Replies.Add(comment);
                            int after = commentDTO.Replies.Count;
                            return after - before;
                        }
                    }

                    throw new ArgumentOutOfRangeException("Comment not found.");
                }
            }
            throw new ArgumentOutOfRangeException("Post not found.");
        }

        public int Upload(CommentDTO dto)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<CommentDTO> GetFromComment(int parentCommentID)
        {
            throw new NotImplementedException();
        }

        public List<CommentDTO> GetFromPost(int postID)
        {
            throw new NotImplementedException();
        }

        public List<CommentDTO> LoadAll()
        {
            throw new NotImplementedException();
        }

        public int Update(CommentDTO dto)
        {
            throw new NotImplementedException();
        }

    }
}

