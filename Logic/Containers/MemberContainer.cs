using System;
using Data_Access.Interfaces;
using Logic.Users;

namespace Logic.Containers
{
    public class MemberContainer
    {
        private IMemberDAL DAL;
        public MemberContainer(IMemberDAL dal)
        {
            DAL = dal;
        }

        public Member Get(string username, string password)
        {
            Member member = new Member(DAL, DAL.Get(username, password));
            if (member.ID > 0)
            {
                return null;
            }

            return member;

        }
    }
}
