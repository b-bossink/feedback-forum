using Data_Access;
using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ICategoryDAL
    {
        public bool Upload(CategoryDTO category);
        public List<CategoryDTO> LoadAll();
        public CategoryDTO Load(int id);
    }
}
