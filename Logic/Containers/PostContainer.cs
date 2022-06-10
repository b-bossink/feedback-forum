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

        /// <returns>The post with the given ID. Null when ID is not found.</returns>
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

        public CommunicationResult Delete(int id)
        {
            if (!_DAL.Exists(id))
            {
                return CommunicationResult.PostNotFoundError;
            }

            int rowsSaved = _DAL.Delete(id);
            if (rowsSaved == 1)
            {
                return CommunicationResult.Succes;
            }
            else
            {
                return CommunicationResult.UnexpectedError;
            }
        }
    }
}
