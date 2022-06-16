using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Interfaces.Logic;
using UnitTest.STUBs;
using Interfaces.DTOs;
using UnitTest.TestEntities;
using Logic.Entities;
using UnitTest.TestContainers;
using System;
using Attribute = Logic.Entities.Attribute;

namespace UnitTest
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void SavePostSuccesfully()
        {
            // Arrange
            TestCategory category = new TestCategory(new CategorySTUB(), "Testcategorie", new List<Attribute>());
            Dictionary<Attribute, string> attributes = new Dictionary<Attribute, string>();
            foreach (Attribute attribute in category.Attributes)
            {
                attributes.Add(attribute, "Testwaarde");
            }
            TestMember user = new TestMember(new MemberSTUB(), "test_gebruiker", "gebruiker@email.nl", "wachtwoord123", 12);

            PostSTUB stub = new PostSTUB();
            TestPost post = new TestPost(stub, "Test Post", System.DateTime.Now, new List<CommentFactory>(), 0,
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
            PostSTUB stub = new PostSTUB();
            TestPostContainer container = new TestPostContainer(stub);
            TestPost[] posts;
            int expectedPostID = 123;
            

            // Act
            posts = Array.ConvertAll(container.GetAll(), post => (TestPost)post);

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
            PostSTUB stub = new PostSTUB();
            TestPostContainer container = new TestPostContainer(stub);
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
            PostSTUB stub = new PostSTUB();
            TestPostContainer container = new TestPostContainer(stub);
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
            TestPost oldPost = new TestPost(stub.database[0]);
            string expectedName = "succesfully edited";
            CommunicationResult result;
            CommunicationResult expectedResult = CommunicationResult.Succes;
            TestPost newPost = new TestPost(stub, expectedName, oldPost.CreationDate, oldPost.Comments, oldPost.Upvotes, oldPost.Category, oldPost.ValuesByAttributes, oldPost.Owner, oldPost.ID);

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
            TestPost oldPost = new TestPost(stub.database[0]);
            CommunicationResult result;
            CommunicationResult expectedResult = CommunicationResult.PostNotFoundError;
            TestPost newPost = new TestPost(stub, "new post", oldPost.CreationDate, oldPost.Comments, oldPost.Upvotes, oldPost.Category, oldPost.ValuesByAttributes, oldPost.Owner, fakeId);

            // Act
            result = newPost.Update();

            // Assert
            Assert.AreEqual(expectedResult, result, "Either none or too many rows have been updated.");
          }
    }
}
