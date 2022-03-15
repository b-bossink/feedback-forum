using Microsoft.VisualStudio.TestTools.UnitTesting;
using FeedbackForum.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackForumUnitTests
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void SetAttribute()
        {
            // Assign
            string attributeName = "Test Attribuut";
            Post post = new Post("Test 1 2 3", new Category("Test 1 2 3", new List<string>()
            { 
                attributeName
            }));
            string expected = "waarde";

            // Act
            post.SetAttributeValue(attributeName, expected);

            // Assert
            Assert.AreEqual(expected, post.Attributes[attributeName]);
        }

        [TestMethod]
        public void AddComment()
        {
            // Assign
            Post post = new Post("Test 1 2 3", new Category("Test 1 2 3", new List<string>()));
            Comment newComment = new Comment("Dit is een test-comment.");

            // Act
            post.Add(newComment);

            // Assert
            Assert.IsTrue(post.Comments.Contains(newComment), "Couldn't find comment in List.");
        }

        [TestMethod]
        public void RemoveComment()
        {
            // Assign
            Post post = new Post("Test 1 2 3", new Category("Test 1 2 3", new List<string>()));
            Comment newComment = new Comment("Dit is een test-comment.");
            post.Add(newComment);

            // Act
            post.Delete(newComment);

            // Assert
            Assert.IsFalse(post.Comments.Contains(newComment), "Comment was found in List.");
        }

        [TestMethod]
        public void AddUpvote()
        {
            // Assign
            Post post = new Post("Test 1 2 3", new Category("Test 1 2 3", new List<string>()));
            int expected = post.Upvotes + 1;

            // Act
            post.Add(1);

            // Assert
            Assert.AreEqual(expected, post.Upvotes, "Upvotes have not increased by 1.");
        }

        [TestMethod]
        public void RemoveUpvote()
        {
            // Assign
            Post post = new Post("Test 1 2 3", new Category("Test 1 2 3", new List<string>()));
            post.Add(1);
            int expected = post.Upvotes - 1;

            // Act
            post.Delete(1);

            // Assert
            Assert.AreEqual(expected, post.Upvotes, "Upvotes have not decreased by 1.");

        }
    }
}
