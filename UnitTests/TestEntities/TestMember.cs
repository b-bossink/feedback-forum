using System;
using Interfaces;
using Interfaces.DTOs;
using Logic.Entities;
using UnitTest.STUBs;

namespace UnitTest.TestEntities
{
	public class TestMember : MemberFactory
	{
        public MemberSTUB STUB { get; private set; }
        public TestMember(MemberDTO dto) : base(dto) { }

        public TestMember(MemberSTUB stub, string username, string email, string password, int id = -1)
            : base(username, email, password, id) {
            STUB = stub;
        }

        protected override IMemberDAL GetDAL()
        {
            return STUB;
        }
    }
}

