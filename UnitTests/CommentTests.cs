using System;
using System.Collections.Generic;
using Interfaces.DTOs;
using Logic;
using Logic.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest.STUBs;

namespace UnitTest
{
    [TestClass]
	public class CommentTests
	{
        [TestMethod]
        public void CommentOnPost()
        {
            // Arrange
            CommentSTUB stub = new CommentSTUB();
            Member user = new Member(new MemberSTUB(), "test_gebruiker", "gebruiker@email.nl", "wachtwoord123", 12);
            Comment comment = new Comment(stub, "Ik ben een test-comment",
                DateTime.Now, 56, new List<Comment>(), user);
            int postId = 123;
            CommunicationResult expectedResult = CommunicationResult.Succes;
            CommunicationResult result;

            // Act
            result = comment.Upload(postId);

            // Assert
            Assert.AreEqual(expectedResult, result, "Either none or too many rows have been saved.");
            foreach (PostDTO postDTO in stub.postStub.database)
            {
                if (postDTO.ID == postId)
                {
                    foreach (CommentDTO commentDTO in postDTO.Comments)
                    {
                        if (commentDTO.ID == comment.ID && commentDTO.CreationDate == comment.CreationDate && commentDTO.Text == comment.Text)
                        {
                            return;
                        }
                    }
                }
            }
            Assert.Fail("Inserted comment's ID, Text and CreationDate could not be found in any comment in post in STUB.");
        }

        [TestMethod]
        public void CommentOnNonExistentPost()
        {
            // Arrange
            CommentSTUB stub = new CommentSTUB();
            Member user = new Member(new MemberSTUB(), "test_gebruiker", "gebruiker@email.nl", "wachtwoord123", 12);
            Comment comment = new Comment(stub, "Ik ben een test-comment",
                DateTime.Now, 56, new List<Comment>(), user);
            int postId = 999;
            CommunicationResult expectedResult = CommunicationResult.PostNotFoundError;
            CommunicationResult result;

            // Act
            result = comment.Upload(postId);

            // Assert
            Assert.AreEqual(expectedResult, result, "Saving comment was expected to fail, but found result: " + result.ToString());
        }
    }
}

