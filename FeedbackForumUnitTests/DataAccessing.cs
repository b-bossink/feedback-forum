using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Logic;
using System;
using Logic.Containers;
using UnitTest.STUBs;

namespace FeedbackForumUnitTests
{
    [TestClass]
    public class DataAccessing
    {
        [TestMethod]
        public void SavePost()
        {
            // Arrange
            Post post = new Post(new PostSTUB(), "Test Post", DateTime.Now, new List<Comment>(), 0,
                new Category(new CategorySTUB(), "", new List<Logic.Attribute>()), new Dictionary<Logic.Attribute, string>());
            int savedID;

            // Act
            savedID = post.Upload();
            bool succesfullySaved = savedID > 0;

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
            Assert.IsTrue(container.GetAll().Count >= minimum, "No posts have been retrieved. Is the database empty?");
        }

        [TestMethod]
        public void DeletePost()
        {
            // Arrange
            PostContainer container = new PostContainer();
            int idToDelete = 71;
            int rowsDeleted;

            // Act
            rowsDeleted = container.Delete(idToDelete);

            // Assert
            Assert.AreEqual(1, rowsDeleted, "No rows or more than one row have been deleted. Is ID present in Database?");
        }

        [TestMethod]
        public void SaveCategory()
        {
            // Arrange
            Category category = new Category(new CategorySTUB(), "Test Categorie", new List<Logic.Attribute>() {
                new Logic.Attribute("Test Attribute")
            });
            bool succesfullySaved;

            // Act
            succesfullySaved = category.Upload();

            // Assert
            Assert.IsTrue(succesfullySaved, "No rows have been saved.");

        }

    }
}
