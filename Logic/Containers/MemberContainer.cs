using System;
using Interfaces;
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

        public Member Get(string username, string password)
        {
            Member member = new Member(_DAL.Get(username, password));
            return member;
        }

        public Member Get(int id)
        {
            return new Member(_DAL.Get(id));
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
