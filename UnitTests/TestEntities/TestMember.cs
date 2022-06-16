using System;
using Interfaces;
using Interfaces.DTOs;
using Logic.Entities;
using UnitTest.STUBs;

namespace UnitTest.TestEntities
{
	public class TestMember : MemberFactory
	{
        public TestMember(MemberDTO dto) : base(dto) { }

        public TestMember(string username, string email, string password, int id = -1)
            : base(username, email, password, id) { }

        protected override IMemberDAL GetDAL()
        {
            return new MemberSTUB();
        }
    }
}

