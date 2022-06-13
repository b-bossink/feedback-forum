using Interfaces;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;

namespace UnitTest.STUBs
{
    public class CategorySTUB : ICategoryDAL
    {
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public CategoryDTO? Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDTO> LoadAll()
        {
            throw new NotImplementedException();
        }

        public int Update(CategoryDTO dto)
        {
            throw new NotImplementedException();
        }

        public int Upload(CategoryDTO category)
        {
            throw new NotImplementedException();
        }
    }
}
