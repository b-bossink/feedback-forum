using System;
using Interfaces;
using Interfaces.DTOs;
using Logic.Containers;
using Logic.Entities;
using UnitTest.STUBs;
using UnitTest.TestEntities;

namespace UnitTest.TestContainers
{
	public class TestMemberContainer : MemberContainerFactory
	{
        public MemberSTUB STUB { get; private set; }

        public TestMemberContainer(MemberSTUB stub)
        {
            STUB = stub;
        }

        protected override MemberFactory CreateMember(MemberDTO dto)
        {
            return new TestMember(dto);
        }

        protected override IMemberDAL GetDAL()
        {
            return STUB;
        }
    }
}

