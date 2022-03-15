using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackForum.Classes
{
    public class CategoryContainer
    {
        public List<Category> Categories { get; private set; }
        private MSSQLConnection database;

        public CategoryContainer()
        {
            Categories = new List<Category>();
        }

        public void Add(Category category)
        {
            Categories.Add(category);
        }

        public void Edit(Category category)
        {

        }

        public void Delete(Category category)
        {
            Categories.Remove(category);
        }
    }
}
