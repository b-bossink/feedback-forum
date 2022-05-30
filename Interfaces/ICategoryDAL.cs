
using Interfaces.DTOs;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ICategoryDAL
    {
        public bool Upload(CategoryDTO category);
        public List<CategoryDTO> LoadAll();
        /// <returns>Null when id is not found.</returns>
        public CategoryDTO? Load(int id);
    }
}
