using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Logic;
using System;
using Logic.Containers;

namespace FeedbackForumUnitTests
{
    [TestClass]
    public class DataAccessing
    {
        [TestMethod]
        public void SavePost()
        {
            // Arrange
            Post post = new Post("Test Post", DateTime.Now, new List<Comment>(), 0,
                new Category("", new List<Logic.Attribute>()), new Dictionary<Logic.Attribute, string>());
            bool succesfullySaved;

            // Act
            succesfullySaved = post.Upload();

            // Assert
            Assert.IsTrue(succesfullySaved, "No rows have been saved.");

        }

        [TestMethod]
        public void LoadAllPosts()
        {
            // Arrange
            int minimum = 1;

            // Act
            PostContainer container = new PostContainer();

            // Assert
            Assert.IsTrue(container.Posts.Count >= minimum, "No posts have been retrieved. Is the database empty?");
        }
    }
}
