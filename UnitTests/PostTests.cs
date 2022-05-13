using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Logic;
using System;
using Logic.Containers;
using UnitTest.STUBs;
using Logic.Users;
using Interfaces.DTOs;
using System.Diagnostics;
using Interfaces;

namespace FeedbackForumUnitTests
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void SavePost()
        {
            // Arrange
            Category category = new Category(new CategorySTUB(), "Testcategorie", new List<Logic.Attribute>());
            Dictionary<Logic.Attribute, string> attributes = new Dictionary<Logic.Attribute, string>();
            foreach (Logic.Attribute attribute in category.Attributes)
            {
                attributes.Add(attribute, "Testwaarde");
            }
            Member user = new Member(12, "test_gebruiker", "gebruiker@email.nl", "wachtwoord123");
            
            PostSTUB stub = new PostSTUB();
            Post post = new Post(stub, "Test Post", DateTime.Now, new List<Comment>(), 0,
                category, attributes, user);
            int rowsSaved;
            int expectedRowsSaved = 1;

            // Act
            rowsSaved = post.Upload();

            // Assert
            Assert.AreEqual(expectedRowsSaved, rowsSaved, "Either none or too many rows have been saved.");
            foreach (PostDTO dto in stub.database)
            {
                if (post.ID == dto.ID && post.Name == dto.Name && post.CreationDate == dto.CreationDate)
                    return;
            }
            Assert.Fail("Inserted post's ID, Name and CreationDate could not be found in any post in STUB.");
        }

        [TestMethod]
        public void LoadAllPosts()
        {
            // Arrange
            int minimum = 1;
            PostSTUB stub = new PostSTUB();
            PostContainer container = new PostContainer(stub);
            List<Post> posts;
            int expectedPostID = 123;
            

            // Act
            posts = container.GetAll();

            // Assert
            Assert.IsTrue(posts.Count >= minimum, "Too few posts have been retrieved.");
            foreach (PostDTO dto in stub.database)
            {
                if (dto.ID == expectedPostID)
                    return;
            }
            Assert.Fail("Expected ID could not be found in any post in STUB.");

        }

        [TestMethod]
        public void DeletePost()
        {
            // Arrange
            PostSTUB stub = new PostSTUB();
            PostContainer container = new PostContainer(stub);
            int postId = 123;
            int rowsDeleted;
            int expectedRowsDeleted = 1;

            // Act
            rowsDeleted = container.Delete(postId);

            // Assert
            Assert.AreEqual(expectedRowsDeleted, rowsDeleted, "Either none or too many rows have been deleted.");
            foreach (PostDTO dto in stub.database)
            {
                if (dto.ID == postId)
                    Assert.Fail("ID expected to be deleted was still found in STUB");
            }

        }

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
