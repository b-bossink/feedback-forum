using Interfaces.DTOs;
using Logic;
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
        public void RegisterUserSuccesfully()
        {
            // Arrange
            MemberSTUB stub = new MemberSTUB();
            Member member = new Member(stub,
                "nieuwe_gebruiker",
                "user@email.com",
                "mijn_wachtwoord");
            CommunicationResult expectedResult = CommunicationResult.Succes;
            CommunicationResult result;

            // Act
            result = member.Create();

            // Assert
            Assert.AreEqual(expectedResult, result, "Either none or too many rows have been saved.");
            foreach (MemberDTO dto in stub.database)
            {
                if (member.ID == dto.ID
                    && member.Username == dto.Username
                    && member.Emailaddress == dto.Emailaddress
                    && member.Password == dto.Password)
                {
                    return;
                }
            }
            Assert.Fail("Inserted member's ID, Username, Password and Emailaddress combination could not be found with any member in STUB.");
        }

        [TestMethod]
        public void RegisterUserWithExistingUsername()
        {
            // Arrange
            MemberSTUB stub = new MemberSTUB();
            Member member = new Member(stub,
                "test_gebruiker",
                "user@email.com",
                "mijn_wachtwoord");
            CommunicationResult expectedResult = CommunicationResult.DuplicateUsernameError;
            CommunicationResult result;

            // Act
            result = member.Create();

            // Assert
            Assert.AreEqual(expectedResult, result, "Expected result to be 'username taken' error, but test returned " + result.ToString());
        }


        [TestMethod]
        public void RegisterUserWithExistingEmailaddress()
        {
            // Arrange
            MemberSTUB stub = new MemberSTUB();
            Member member = new Member(stub,
                "nieuwe_gebruiker",
                "gebruiker@email.nl",
                "mijn_wachtwoord");
            CommunicationResult expectedResult = CommunicationResult.DuplicateEmailError;
            CommunicationResult result;

            // Act
            result = member.Create();

            // Assert
            Assert.AreEqual(expectedResult, result, "Expected result to be 'email taken' error, but test returned " + result.ToString());
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
    }
}

