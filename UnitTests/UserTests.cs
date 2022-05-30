using System;
using Interfaces.DTOs;
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
        public void RegisterUser()
        {
            // Arrange
            MemberSTUB stub = new MemberSTUB();
            Member member = new Member(stub,
                "nieuwe_gebruiker",
                "user@email.com",
                "mijn_wachtwoord");
            int rowsSaved;
            int expectedRowsSaved = 1;

            // Act
            rowsSaved = member.Register();

            // Assert
            Assert.AreEqual(expectedRowsSaved, rowsSaved, "Either none or too many rows have been saved.");
            foreach (MemberDTO dto in stub.database)
            {
                if (member.ID == dto.ID
                    && member.Username == dto.Username
                    && member.Emailaddress == dto.Emailaddress
                    && member.Password == dto.Password)
                    return;
            }
            Assert.Fail("Inserted member's ID, Username, Password and Emailaddress combination could not be found with any member in STUB.");
        }


        [TestMethod]
        public void GetUserByCredentials()
        {
            // Arrange
            MemberContainer container = new MemberContainer(new MemberSTUB());
            int id = 1;
            string username = "test_gebruiker";
            string password = "wachtwoord123";
            string email = "gebruiker@email.nl";

            // Act
            Member member = container.Get(username, password);

            // Assert
            Assert.AreEqual(member.ID, id, "Retrieved member's ID did not match.");
            Assert.AreEqual(member.Username, username, "Retrieved member's username did not match.");
            Assert.AreEqual(member.Emailaddress, email, "Retrieved member's emailadress did not match.");
            Assert.AreEqual(member.Password, password, "Retrieved member's password did not match.");
        }

        [TestMethod]
        public void GetUserWithIncorrectCredentials()
        {
            // Arrange
            MemberContainer container = new MemberContainer(new MemberSTUB());
            string username = "nep_gebruiker";
            string password = "onjuist_wachtwoord";
            Member expectedMember = null;

            // Act
            Member member = container.Get(username, password);

            // Assert
            Assert.AreEqual(member, expectedMember, "Due to invalid credentials, the returned member should be null.");
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
        public void UsernameExists()
        {
            // Arrange
            MemberContainer container = new MemberContainer(new MemberSTUB());
            string username = "test_gebruiker";
            bool expectedResult = true;
            bool result;

            // Act
            result = container.UsernameExists(username);

            // Assert
            Assert.AreEqual(expectedResult, result, "Expected result to be true, but test didnt return true");
        }

        [TestMethod]
        public void UsernameDoesntExist()
        {
            // Arrange
            MemberContainer container = new MemberContainer(new MemberSTUB());
            string username = "nep_gebruiker";
            bool expectedResult = false;
            bool result;

            // Act
            result = container.UsernameExists(username);

            // Assert
            Assert.AreEqual(expectedResult, result, "Expected result to be false, but test didnt return false");
        }

        [TestMethod]
        public void EmailExists()
        {
            // Arrange
            MemberContainer container = new MemberContainer(new MemberSTUB());
            string email = "gebruiker@email.nl";
            bool expectedResult = true;
            bool result;

            // Act
            result = container.EmailExists(email);

            // Assert
            Assert.AreEqual(expectedResult, result, "Expected result to be true, but test didnt return true");
        }

        [TestMethod]
        public void EmailDoesntExist()
        {
            // Arrange
            MemberContainer container = new MemberContainer(new MemberSTUB());
            string email = "nepgebruiker@email.nl";
            bool expectedResult = false;
            bool result;

            // Act
            result = container.EmailExists(email);

            // Assert
            Assert.AreEqual(expectedResult, result, "Expected result to be false, but test didnt return false");
        }
    }
}

