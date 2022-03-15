using Microsoft.VisualStudio.TestTools.UnitTesting;
using FeedbackForum.Classes;
using System.Collections.Generic;

namespace FeedbackForumUnitTests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void AddCategory()
        {
            // Assign
            CategoryContainer container = new CategoryContainer();
            Category testCategory = new Category("Muziek", new List<string>());

            // Act
            container.Add(testCategory);

            // Assert
            Assert.IsTrue(container.Categories.Contains(testCategory), $"Couldn't find {testCategory.Name} in CategoryContainer.");
        }

        [TestMethod]
        public void RemoveCategory()
        {
            // Assign
            CategoryContainer container = new CategoryContainer();
            Category testCategory = new Category("Muziek", new List<string>());
            container.Add(testCategory);

            // Act
            container.Delete(testCategory);

            // Assert
            Assert.IsFalse(container.Categories.Contains(testCategory), $"Category {testCategory.Name} still exists in CategoryContainer.");
        }

        [TestMethod]
        public void AddPost()
        {
            // Assign
            PostContainer container = new PostContainer();
            Post testPost = new Post("Test 1 2 3", new Category("Muziek", new List<string>()));

            // Act
            container.Add(testPost);

            // Assert
            Assert.IsTrue(container.Posts.Contains(testPost), $"Couldn't find {testPost.Name} in PostContainer.");
        }

        [TestMethod]
        public void EditPost()
        {

        }

        [TestMethod]
        public void RemovePost()
        {
            // Assign
            PostContainer container = new PostContainer();
            Post testPost = new Post("Test 1 2 3", new Category("Muziek", new List<string>()));
            container.Add(testPost);

            // Act
            container.Delete(testPost);

            // Assert
            Assert.IsFalse(container.Posts.Contains(testPost), $"Post {testPost.Name} still exists in PostContainer.");
        }
    }
}
