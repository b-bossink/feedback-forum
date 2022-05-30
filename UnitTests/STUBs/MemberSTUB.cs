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

        public MemberDTO? Get(string username, string password)
        {
            foreach (MemberDTO member in database) {
                if (member.Username == username && member.Password == password)
                {
                    return member;
                }
            }
            return null;
        }

        public MemberDTO? Get(int id)
        {
            foreach (MemberDTO member in database)
            {
                if (member.ID == id)
                {
                    return member;
                }
            }
            return null;
        }

        public int RegisterNew(MemberDTO member)
        {
            int before = database.Count;
            database.Add(member);
            int after = database.Count;
            return after - before;
        }

        public void Update(MemberDTO member)
        {
            throw new NotImplementedException();
        }

        public bool UsernameExists(string username)
        {
            foreach (MemberDTO dto in database)
            {
                if (dto.Username.Equals(username))
                {
                    return true;
                }
            }
            return false;
        }

        public bool EmailExists(string email)
        {
            foreach (MemberDTO dto in database)
            {
                if (dto.Emailaddress.Equals(email))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

