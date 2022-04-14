using Data_Access;
using Data_Access.DTOs;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.STUBs
{
    class CategorySTUB : ICategoryDAL
    {
        public CategoryDTO Load(int id)
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
