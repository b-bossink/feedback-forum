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
            return member;
        }

        public Member Get(int id)
        {
            return new Member(DAL, DAL.Get(id));
        }

        public bool ValidateCredentials(string username, string password)
        {
            try
            {
                Get(username, password);
            }
            catch (System.Security.Authentication.InvalidCredentialException)
            {
                return false;
            }
            return true;
        }
    }
}
