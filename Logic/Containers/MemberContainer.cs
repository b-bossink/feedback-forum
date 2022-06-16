using System;
using Data_Access;
using Interfaces;
using Interfaces.DTOs;
using Logic.Entities;

namespace Logic.Containers
{
    public class MemberContainer : MemberContainerFactory
    {
        protected override MemberFactory CreateMember(MemberDTO dto)
        {
            return new Member(dto);
        }

        protected override IMemberDAL GetDAL()
        {
            return new MemberDAL();
        }
    }
}

