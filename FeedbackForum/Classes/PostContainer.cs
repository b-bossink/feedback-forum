using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackForum.Classes
{
    class PostContainer
    {
        public List<Post> Posts { get; private set; }
        private Database database;

        public PostContainer()
        {
            Posts = new List<Post>();
        }

        public void Add(Post post)
        {
            Posts.Add(post);
        }

        public void Edit(Post post)
        {
            for (int i = 0; i < Posts.Count; i++)
            {
                if (Posts[i].ID == post.ID)
                {
                    Posts[i] = post;
                }
            }
        }

        public void Delete(Post post)
        {
            foreach (Post _post in Posts)
            {
                if (post == _post)
                {
                    Posts.Remove(_post);
                }
            }
        }
    }
}
