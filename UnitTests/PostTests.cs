using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Logic;
using System;
using Logic.Containers;
using UnitTest.STUBs;
using Logic.Users;
using Interfaces.DTOs;

namespace UnitTest
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void SavePostSuccesfully()
        {
            // Arrange
            Category category = new Category(new CategorySTUB(), "Testcategorie", new List<Logic.Attribute>());
            Dictionary<Logic.Attribute, string> attributes = new Dictionary<Logic.Attribute, string>();
            foreach (Logic.Attribute attribute in category.Attributes)
            {
                attributes.Add(attribute, "Testwaarde");
            }
            Member user = new Member(new MemberSTUB(), "test_gebruiker", "gebruiker@email.nl", "wachtwoord123", 12);
            
            PostSTUB stub = new PostSTUB();
            Post post = new Post(stub, "Test Post", DateTime.Now, new List<Comment>(), 0,
                category, attributes, user);

            Post.CommunicationResult result;
            Post.CommunicationResult expectedResult = Post.CommunicationResult.Succes;

            // Act
            result = post.Upload();

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
            PostContainer container = new PostContainer(stub);
            int postId = 123;
            Post.CommunicationResult result;
            Post.CommunicationResult expectedResult = Post.CommunicationResult.Succes;

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
            PostContainer container = new PostContainer(stub);
            int postId = 999;
            Post.CommunicationResult result;
            Post.CommunicationResult expectedResult = Post.CommunicationResult.PostNotFoundError;

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
            Post.CommunicationResult result;
            Post.CommunicationResult expectedResult = Post.CommunicationResult.Succes;
            Post newPost = new Post(stub, expectedName, oldPost.CreationDate, oldPost.Comments, oldPost.Upvotes, oldPost.Category, oldPost.ValuesByAttributes, oldPost.Owner, oldPost.ID);

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
            Post.CommunicationResult result;
            Post.CommunicationResult expectedResult = Post.CommunicationResult.PostNotFoundError;
            Post newPost = new Post(stub, "new post", oldPost.CreationDate, oldPost.Comments, oldPost.Upvotes, oldPost.Category, oldPost.ValuesByAttributes, oldPost.Owner, fakeId);

            // Act
            result = newPost.Update();

            // Assert
            Assert.AreEqual(expectedResult, result, "Either none or too many rows have been updated.");
          }
    }
}
