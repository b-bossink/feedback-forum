using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackForum.Classes
{
    class Database
    {
        private string connectionString;
        public Database(string connection)
        {
            connectionString = connection;
        }

        public void Update(Post post)
        {

        }

        public void Update(Category category)
        {

        }

        public List<Post> LoadPosts()
        {
            return null;
        }

        public List<Category> LoadCategories()
        {
            return null;
        }
    }
}
