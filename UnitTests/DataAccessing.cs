using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Logic;
using System;
using Logic.Containers;
using UnitTest.STUBs;
using Logic.Users;

namespace FeedbackForumUnitTests
{
    [TestClass]
    public class DataAccessing
    {
        [TestMethod]
        public void SavePost()
        {
            // Arrange

            Category category = new Category(new CategorySTUB(), "", new List<Logic.Attribute>());
            Dictionary<Logic.Attribute, string> attributes = new Dictionary<Logic.Attribute, string>();
            foreach (Logic.Attribute attribute in category.Attributes)
            {
                attributes.Add(attribute, "Testwaarde");
            }

            Post post = new Post(new PostSTUB(), "Test Post", DateTime.Now, new List<Comment>(), 0,
                category, attributes);
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
            PostContainer container = new PostContainer(new PostSTUB());
            List<Post> posts;

            // Act
            posts = container.GetAll();

            // Assert
            Assert.IsTrue(posts.Count >= minimum, "Too few posts have been retrieved.");
        }

        [TestMethod]
        public void CommentOnPost()
        {
            // Arrange
            Comment comment = new Comment(new CommentSTUB(), "Ik ben een test-comment",
                DateTime.Now, 56, new List<Comment>());
            int postId = 1;
            int expectedResult = 1;
            int result;

            // Act
            result = comment.Upload(postId);

            // Assert
            Assert.AreEqual(expectedResult, result, "Either none or too many rows have been saved.");
        }

        [TestMethod]
        public void CommentOnComment()
        {
            // Arrange
            Comment comment = new Comment(new CommentSTUB(), "Ik ben een test-comment onder een andere comment.",
                DateTime.Now, 22,new List<Comment>());
            int postId = 1;
            int commentId = 1;
            int expectedResult = 1;
            int result;

            // Act
            result = comment.Upload(postId, commentId);

            // Assert
            Assert.AreEqual(expectedResult, result, "Either none or too many rows have been saved.");
        }

        //[TestMethod]
        //public void DeletePost()
        //{
        //    // Arrange
        //    Post post = new Post(new PostSTUB(), "Edited Test Post", DateTime.Now, new List<Comment>(), 0,
        //        new Category(new CategorySTUB(), "", new List<Logic.Attribute>()), new Dictionary<Logic.Attribute, string>(), 1);
        //    int rowsDeleted;
        //    int expectedRowsDeleted = 1;

        //    // Act
        //    rowsDeleted = post.Delete();

        //    // Assert
        //    Assert.AreEqual(expectedRowsDeleted, rowsDeleted, "Either none or too many rows have been deleted.");
        //}

        //[TestMethod]
        //public void SaveCategory()
        //{
        //    // Arrange
        //    Category category = new Category(new CategorySTUB(), "Test Categorie", new List<Logic.Attribute>() {
        //        new Logic.Attribute("Test Attribute")
        //    });
        //    bool succesfullySaved;

        //    // Act
        //    succesfullySaved = category.Upload();

        //    // Assert
        //    Assert.IsTrue(succesfullySaved, "No rows have been saved.");
        //}

    }
}
