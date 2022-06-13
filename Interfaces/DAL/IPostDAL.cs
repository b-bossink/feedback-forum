
using Interfaces.DTOs;

namespace Interfaces
{
    public interface IPostDAL : IDAL<PostDTO>
    {
        public bool Exists(int id);
    }
}
