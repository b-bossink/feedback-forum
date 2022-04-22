using System;
using Interfaces;
using Interfaces.DTOs;

namespace UnitTest.STUBs
{
    public class MemberSTUB : IMemberDAL
    {
        public void Add(MemberDTO member)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public MemberDTO Get(string username, string password)
        {
            throw new NotImplementedException();
        }

        public MemberDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(MemberDTO member)
        {
            throw new NotImplementedException();
        }
    }
}

