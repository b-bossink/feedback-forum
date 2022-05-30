using Interfaces.DTOs;

namespace Interfaces
{
    public interface IMemberDAL
    {
        /// <returns>Null when credentials are invalid.</returns>
        public MemberDTO? Get(string username, string password);
        /// <returns>Null when id is not found.</returns>
        public MemberDTO? Get(int id);
        public int RegisterNew(MemberDTO member);
        public void Update(MemberDTO member);
        public bool UsernameExists(string username);
        public bool EmailExists(string email);
    }
}
