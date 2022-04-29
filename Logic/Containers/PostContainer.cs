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
            List<PostDTO> dtos = DAL.LoadAll();
            if (dtos != null)
            {
                List<Post> result = new List<Post>();
                foreach (PostDTO dto in dtos)
                {
                    result.Add(new Post(new PostDAL(), new CategoryDAL(), new CommentDAL(), new MemberDAL(), dto));
                }
                return result;
            }
            return null;
        }

        public Post Get(int id)
        {
            List<PostDTO> dtos = DAL.LoadAll();
            if (dtos != null)
            {
                foreach (PostDTO dto in dtos)
                {
                    Post post = new Post(new PostDAL(), new CategoryDAL(), new CommentDAL(), new MemberDAL(), dto);
                    if (post.ID == id)
                    {
                        return post;
                    }
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
