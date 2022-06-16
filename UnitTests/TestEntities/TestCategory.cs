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
        public CategorySTUB STUB { get; private set; }
        public TestCategory(CategoryDTO dto) : base(dto) { }

        public TestCategory(CategorySTUB stub, string name, List<Logic.Entities.Attribute> attributes, int id = -1) : base(name, attributes, id)
        {
            STUB = stub;
        }
       
        protected override ICategoryDAL GetDAL()
        {
            return STUB;
        }
    }
}

