
using Data_Access;
using Interfaces;
using Interfaces.DTOs;

namespace Logic.Entities
{
    public class Member : MemberFactory
    {
        public Member(string username, string email, string password, int id = -1)
            : base(username, email, password, id) { }

        public Member(MemberDTO dto) : base(dto) { }

        protected override IMemberDAL GetDAL()
        {
            return new MemberDAL();
        }
    }
}

