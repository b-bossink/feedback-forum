
using Interfaces.DTOs;

namespace Interfaces
{
    public interface ICategoryDAL : IDAL<CategoryDTO>
    {
        /// <returns>Null when id is not found.</returns>
        public CategoryDTO? Get(int id);
    }
}
