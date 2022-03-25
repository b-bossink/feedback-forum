using Data_Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Containers
{
    public class PostContainer
    {
        public List<Post> Posts { get; private set; }
        private PostDAL DAL = new PostDAL();

        public PostContainer()
        {
            Posts = new List<Post>();
            Refresh();
        }

        public void Refresh()
        {
            Posts.Clear();
            foreach (PostDTO dto in DAL.LoadAll())
            {
                Posts.Add(new Post(dto));
            }
        }
    }
}
