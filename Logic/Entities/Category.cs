using System;
using System.Collections.Generic;
using Data_Access;
using Interfaces;
using Interfaces.DTOs;

namespace Logic.Entities
{
    public class Category : CategoryFactory
    {
        public Category(string name, List<Attribute> attributes, int id = -1)
            : base(name, attributes, id) { }

        public Category(CategoryDTO dto) : base(dto) { }

        protected override ICategoryDAL GetDAL()
        {
            return new CategoryDAL();
        }
    }
}

