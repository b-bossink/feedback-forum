using Data_Access;
using Interfaces;
using Interfaces.DTOs;

namespace Logic.Factories
{
    public class PostDALCreator : DALFactory<PostDTO>
    {
        public override IDAL<PostDTO> GetDAL()
        {
            return new PostDAL();
        }
    }
}

