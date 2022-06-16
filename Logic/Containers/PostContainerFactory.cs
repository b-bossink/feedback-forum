using System.Collections.Generic;
using Interfaces;
using Interfaces.DTOs;
using Interfaces.Logic;
using Logic.Entities;

namespace Logic.Containers
{
    public abstract class PostContainerFactory : IContainer<PostDTO>
    {
        public IEntity<PostDTO>[] GetAll()
        {
            List<PostDTO> dtos = GetDAL().LoadAll();
            if (dtos != null)
            {
                List<PostFactory> result = new List<PostFactory>();
                foreach (PostDTO dto in dtos)
                {
                    result.Add(CreatePost(dto));
                }
                return result.ToArray();
            }
            return new PostFactory[0];
        }

        public IEntity<PostDTO> Get(int id)
        {
            List<PostDTO> dtos = GetDAL().LoadAll();
            if (dtos != null)
            {
                foreach (PostDTO dto in dtos)
                {
                    PostFactory post = CreatePost(dto);
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
            if (!GetDAL().Exists(id))
            {
                return CommunicationResult.PostNotFoundError;
            }

            int rowsSaved = GetDAL().Delete(id);
            if (rowsSaved == 1)
            {
                return CommunicationResult.Succes;
            }
            else
            {
                return CommunicationResult.UnexpectedError;
            }
        }

        protected abstract IPostDAL GetDAL();
        protected abstract PostFactory CreatePost(PostDTO dto);
    }
}
