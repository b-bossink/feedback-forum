using System.Collections.Generic;
using Data_Access.DTOs;

namespace Data_Access.Interfaces
{
    public interface IMemberDAL
    {
        public MemberDTO Get(string username, string password);
        public MemberDTO Get(int id);
        public void Add(MemberDTO member);
        public void Update(MemberDTO member);
        public void Delete();
    }
}
