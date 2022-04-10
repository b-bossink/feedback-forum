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
            int rowsSaved;
            int expectedRowsSaved = 1;

            // Act
            rowsSaved = post.Upload();

            // Assert
            Assert.AreEqual(expectedRowsSaved, rowsSaved, "Either none or too many rows have been saved");

        }

        [TestMethod]
        public void LoadAllPosts()
        {
            // Arrange
            int minimum = 1;

            // Act
            PostContainer container = new PostContainer(new PostSTUB());

            // Assert
            Assert.IsTrue(container.GetAll().Count >= minimum, "No posts have been retrieved.");
        }

        [TestMethod]
        public void DeletePost()
        {
            // Arrange
            Post post = new Post(new PostSTUB(), "Edited Test Post", DateTime.Now, new List<Comment>(), 0,
                new Category(new CategorySTUB(), "", new List<Logic.Attribute>()), new Dictionary<Logic.Attribute, string>(), 1);
            int rowsDeleted;
            int expectedRowsDeleted = 1;

            // Act
            //rowsDeleted = post.Delete();

            // Assert
            Assert.AreEqual(expectedRowsDeleted, rowsDeleted, "Either none or too many rows have been deleted.");
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
