using System;
using Logic.Containers;
using Logic.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest.STUBs;

namespace UnitTest
{
    [TestClass]
	public class UserTests
	{

        [TestMethod]
        public void GetUserByCredentials()
        {
            // Arrange
            MemberContainer container = new MemberContainer(new MemberSTUB());
            string username = "test_gebruiker";
            string password = "wachtwoord123";
            string email = "gebruiker@email.nl";

            // Act
            Member member = container.Get(username, password);

            // Assert
            Assert.AreEqual(member.Username, username, "Retrieved member's username did not match.");
            Assert.AreEqual(member.Emailaddress, email, "Retrieved member's emailadress did not match.");
            Assert.AreEqual(member.Password, password, "Retrieved member's password did not match.");
        }

        [TestMethod]
        public void GetUserByID()
        {
            // Arrange
            MemberContainer container = new MemberContainer(new MemberSTUB());
            int id = 1;
            string username = "test_gebruiker";
            string password = "wachtwoord123";
            string email = "gebruiker@email.nl";

            // Act
            Member member = container.Get(id);

            // Assert
            Assert.AreEqual(member.Username, username, "Retrieved member's username did not match.");
            Assert.AreEqual(member.Emailaddress, email, "Retrieved member's emailadress did not match.");
            Assert.AreEqual(member.Password, password, "Retrieved member's password did not match.");
        }

        [TestMethod]
        public void ValidateCredentials()
        {
            // Arrange
            MemberContainer container = new MemberContainer(new MemberSTUB());
            string username = "test_gebruiker";
            string password = "wachtwoord123";
            bool expectedResult = true;
            bool result;

            // Act
            result = container.ValidateCredentials(username, password);

            // Assert
            Assert.AreEqual(expectedResult, result, "Credential combination is expected to be found, though it failed.");
        }
    }
}

