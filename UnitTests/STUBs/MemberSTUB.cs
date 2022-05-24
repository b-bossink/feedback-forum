using System;
using System.Collections.Generic;
using Interfaces;
using Interfaces.DTOs;

namespace UnitTest.STUBs
{
    public class MemberSTUB : IMemberDAL
    {
        public List<MemberDTO> database;
        public MemberSTUB()
        {
            database = new List<MemberDTO>()
            {
                new MemberDTO
                {
                    ID = 1,
                    Username = "test_gebruiker",
                    Emailaddress = "gebruiker@email.nl",
                    Password = "wachtwoord123"
                }
            };
        }

        public MemberDTO Get(string username, string password)
        {
            foreach (MemberDTO member in database) {
                if (member.Username == username && member.Password == password)
                {
                    return member;
                }
            }
            throw new System.Security.Authentication.InvalidCredentialException();
        }

        public MemberDTO Get(int id)
        {
            foreach (MemberDTO member in database)
            {
                if (member.ID == id)
                {
                    return member;
                }
            }
            return new MemberDTO();
        }

        public int RegisterNew(MemberDTO member)
        {
            throw new NotImplementedException();
        }

        public void Update(MemberDTO member)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string username)
        {
            throw new NotImplementedException();
        }
    }
}

