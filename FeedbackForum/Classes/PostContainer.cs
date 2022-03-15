using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackForum.Classes
{
    public class PostContainer
    {
        public List<Post> Posts { get; private set; }
        private MSSQLConnection sqlConnection = new MSSQLConnection();

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
            Posts.Remove(post);
        }

        public void SetList()
        {
            Posts = sqlConnection.LoadPosts();
            foreach (Post post in Posts)
            {
                Console.WriteLine($"\n\n\n\n\nThis post is named '{post.Name}'. Its category is {post.Category.Name}." +
                      $"Its creation date is {post.CreationDate}.\nIts attributes:");
                foreach (KeyValuePair<Attribute,string> kvp in post.ValuesByAttributes)
                {
                    Console.WriteLine($"- {kvp.Key.Name} | VALUE = {kvp.Value}");
                }
            }
                
        }
    }
}
