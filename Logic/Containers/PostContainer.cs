using Interfaces;
using Interfaces.DTOs;
using System.Collections.Generic;

namespace Logic.Containers
{
    public class PostContainer : IContainer<PostDTO>
    {
        private readonly IPostDAL _DAL;

        public PostContainer(IPostDAL dal)
        {
            _DAL = dal;
        }

        public IEntity<PostDTO>[] GetAll()
        {
            List<PostDTO> dtos = _DAL.LoadAll();
            if (dtos != null)
            {
                List<Post> result = new List<Post>();
                foreach (PostDTO dto in dtos)
                {
                    result.Add(new Post(dto));
                }
                return result.ToArray();
            }
            return null;
        }

        public IEntity<PostDTO> Get(int id)
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
