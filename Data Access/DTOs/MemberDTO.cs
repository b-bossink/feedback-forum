using System.Collections.Generic;

namespace Data_Access.DTOs
{
    public struct MemberDTO
    {
        public int ID;
        public string Username;
        public string Emailaddress;
        public string Password;
        public List<PostDTO> Posts;
        public List<CommentDTO> Comments;
    }
}
