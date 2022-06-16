using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Interfaces.Logic;
using UnitTest.STUBs;
using Interfaces.DTOs;
using Logic.Containers;
using UnitTest.TestEntities;
using Logic.Entities;

namespace UnitTest
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void SavePostSuccesfully()
        {
            // Arrange
            TestCategory category = new TestCategory("Testcategorie", new List<Attribute>());
            Dictionary<Attribute, string> attributes = new Dictionary<Attribute, string>();
            foreach (Attribute attribute in category.Attributes)
            {
                attributes.Add(attribute, "Testwaarde");
            }
            TestMember user = new TestMember("test_gebruiker", "gebruiker@email.nl", "wachtwoord123", 12);
            
            TestPost post = new TestPost("Test Post", System.DateTime.Now, new List<CommentFactory>(), 0,
                category, attributes, user);

            CommunicationResult result;
            CommunicationResult expectedResult = CommunicationResult.Succes;

            // Act
            result = post.Create();

            // Assert
            Assert.AreEqual(expectedResult, result, "Either none or too many rows have been saved.");
            foreach (PostDTO dto in stub.database)
            {
                if (post.ID == dto.ID && post.Name == dto.Name && post.CreationDate == dto.CreationDate)
                {
                    return;
                }
            }
            Assert.Fail("Inserted post's ID, Name and CreationDate could not be found in any post in STUB.");
        }

        [TestMethod]
        public void LoadAllPostsSuccesfully()
        {
            // Arrange
            int minimum = 1;
            PostContainer container = new PostContainer();
            Post[] posts;
            int expectedPostID = 123;
            

            // Act
            posts = (Post[])container.GetAll();

            // Assert
            Assert.IsTrue(posts.Length >= minimum, "Too few posts have been retrieved.");
            foreach (PostDTO dto in stub.database)
            {
                if (dto.ID == expectedPostID)
                {
                    return;
                }
            }
            Assert.Fail("Expected ID could not be found in any post in STUB.");

        }

        [TestMethod]
        public void DeletePostSuccesfully()
        {
            // Arrange
            PostContainer container = new PostContainer();
            int postId = 123;
            CommunicationResult result;
            CommunicationResult expectedResult = CommunicationResult.Succes;

            // Act
            result = container.Delete(postId);

            // Assert
            Assert.AreEqual(expectedResult, result, "Either none or too many rows have been deleted.");
            foreach (PostDTO dto in stub.database)
            {
                if (dto.ID == postId)
                {
                    Assert.Fail("ID expected to be deleted was still found in STUB");
                }
            }

        }

        [TestMethod]
        public void DeleteNonExistentPost()
        {
            // Arrange
            PostContainer container = new PostContainer();
            int postId = 999;
            CommunicationResult result;
            CommunicationResult expectedResult = CommunicationResult.PostNotFoundError;

            // Act
            result = container.Delete(postId);

            // Assert
            Assert.AreEqual(expectedResult, result, "Result didn't match expected.");

        }

        [TestMethod]
        public void EditPostSuccesfully()
        {
            // Arrange
            PostSTUB stub = new PostSTUB();
            Post oldPost = new Post(stub.database[0]);
            string expectedName = "succesfully edited";
            CommunicationResult result;
            CommunicationResult expectedResult = CommunicationResult.Succes;
            Post newPost = new Post(expectedName, oldPost.CreationDate, oldPost.Comments, oldPost.Upvotes, oldPost.Category, oldPost.ValuesByAttributes, oldPost.Owner, oldPost.ID);

            // Act
            result = newPost.Update();

            // Assert
            Assert.AreEqual(expectedResult, result, "Either none or too many rows have been updated.");
            Assert.AreEqual(expectedName, stub.database[0].Name, "Expected post's name does not match.");
        }

        [TestMethod]
        public void EditNonExistentPost()
        {
            // Arrange
            PostSTUB stub = new PostSTUB();
            int fakeId = 999;
            Post oldPost = new Post(stub.database[0]);
            CommunicationResult result;
            CommunicationResult expectedResult = CommunicationResult.PostNotFoundError;
            Post newPost = new Post("new post", oldPost.CreationDate, oldPost.Comments, oldPost.Upvotes, oldPost.Category, oldPost.ValuesByAttributes, oldPost.Owner, fakeId);

            // Act
            result = newPost.Update();

            // Assert
            Assert.AreEqual(expectedResult, result, "Either none or too many rows have been updated.");
          }
    }
}
