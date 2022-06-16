using Data_Access;
using Interfaces;
using Interfaces.DTOs;
using Logic.Entities;

namespace Logic.Containers
{
    public class PostContainer : PostContainerFactory
    {
        protected override PostFactory CreatePost(PostDTO dto)
        {
            return new Post(dto);
        }

        protected override IPostDAL GetDAL()
        {
            return new PostDAL();
        }
    }
}
