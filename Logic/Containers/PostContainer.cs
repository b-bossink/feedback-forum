using Data_Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Containers
{
    public class PostContainer
    {
        private PostDAL DAL = new PostDAL();

        public PostContainer()
        {
        }

        public List<Post> GetAll()
        {
            List<Post> result = new List<Post>();
            foreach (PostDTO dto in DAL.LoadAll())
            {
                result.Add(new Post(dto));
            }
            return result;
        }

        public Post Get(int id)
        {
            foreach (PostDTO dto in DAL.LoadAll())
            {
                Post post = new Post(dto);
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
