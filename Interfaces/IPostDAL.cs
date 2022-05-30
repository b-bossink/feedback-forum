
using Interfaces.DTOs;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IPostDAL
    {
        public int Upload(PostDTO postDTO);
        public int Delete(int id);
        public int Update(PostDTO postDTO);
        public List<PostDTO> LoadAll();
    }
}
