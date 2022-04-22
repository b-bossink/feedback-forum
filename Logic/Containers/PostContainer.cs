using Data_Access;
using Interfaces;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Containers
{
    public class PostContainer
    {
        private IPostDAL DAL;

        public PostContainer(IPostDAL dal)
        {
            DAL = dal;
        }

        public List<Post> GetAll()
        {
            List<Post> result = new List<Post>();
            foreach (PostDTO dto in DAL.LoadAll())
            {
                result.Add(new Post(new PostDAL(), new CategoryDAL(), new CommentDAL(), new MemberDAL(), dto));
            }
            return result;
        }

        public Post Get(int id)
        {
            foreach (PostDTO dto in DAL.LoadAll())
            {
                Post post = new Post(new PostDAL(), new CategoryDAL(), new CommentDAL(), new MemberDAL(), dto);
                if (post.ID == id)
                {
                    return post;
                }
            }
            return null;
        }
        
        public int Delete(int id)
        {
            return DAL.Delete(id);
        }
    }
}
