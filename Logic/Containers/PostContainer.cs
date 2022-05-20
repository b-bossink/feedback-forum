using Interfaces;
using Interfaces.DTOs;
using System.Collections.Generic;

namespace Logic.Containers
{
    public class PostContainer
    {
        private readonly IPostDAL _DAL;

        public PostContainer(IPostDAL dal)
        {
            _DAL = dal;
        }

        public List<Post> GetAll()
        {
            List<PostDTO> dtos = _DAL.LoadAll();
            if (dtos != null)
            {
                List<Post> result = new List<Post>();
                foreach (PostDTO dto in dtos)
                {
                    result.Add(new Post(dto));
                }
                return result;
            }
            return null;
        }

        public Post Get(int id)
        {
            List<PostDTO> dtos = _DAL.LoadAll();
            if (dtos != null)
            {
                foreach (PostDTO dto in dtos)
                {
                    Post post = new Post(dto);
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
            return _DAL.Delete(id);
        }
    }
}
