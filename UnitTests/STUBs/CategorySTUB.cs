using Interfaces;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;

namespace UnitTest.STUBs
{
    public class CategorySTUB : ICategoryDAL
    {
        public CategoryDTO? Load(int id)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDTO> LoadAll()
        {
            throw new NotImplementedException();
        }

        public bool Upload(CategoryDTO category)
        {
            throw new NotImplementedException();
        }
    }
}
