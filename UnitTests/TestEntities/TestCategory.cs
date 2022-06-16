using System;
using System.Collections.Generic;
using Interfaces;
using Interfaces.DTOs;
using Logic.Entities;
using UnitTest.STUBs;

namespace UnitTest.TestEntities
{
    public class TestCategory : CategoryFactory
    {
        public TestCategory(CategoryDTO dto) : base(dto) { }

        public TestCategory(string name, List<Logic.Entities.Attribute> attributes, int id = -1) : base(name, attributes, id) { }
       
        protected override ICategoryDAL GetDAL()
        {
            return new CategorySTUB();
        }
    }
}

