using System;
using Interfaces;
using Interfaces.DTOs;
using Logic.Users;

namespace Logic.Containers
{
    public class MemberContainer
    {
        private readonly IMemberDAL _DAL;
        public MemberContainer(IMemberDAL dal)
        {
            _DAL = dal;
        }

        /// <returns>The member based on the given credentials. Null when credentials are invalid.</returns>
        public Member Get(string username, string password)
        {
            MemberDTO? dto = _DAL.Get(username, password);
            if (dto == null)
                return null;

            return new Member((MemberDTO)dto);
        }

        /// <returns>The member with the given ID. Null when ID is not found.</returns>
        public Member Get(int id)
        {
            MemberDTO? dto = _DAL.Get(id);
            if (dto == null)
                return null;

            return new Member((MemberDTO)dto);
        }
    }
}
