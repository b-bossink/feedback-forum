using Interfaces;
using Interfaces.DTOs;

namespace Logic.Factories
{
    public abstract class DALFactory<T> where T : DTO
    {
        public abstract IDAL<T> GetDAL();
    }
}
