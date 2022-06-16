using System;
using Interfaces;
using Interfaces.DTOs;
using Logic.Containers;
using Logic.Entities;
using UnitTest.STUBs;
using UnitTest.TestEntities;

namespace UnitTest.TestContainers
{
    public class TestCategoryContainer : CategoryContainerFactory
    {
        public CategorySTUB STUB { get; private set; }

        protected override CategoryFactory CreateCategory(CategoryDTO dto)
        {
            return new TestCategory(dto);
        }

        protected override ICategoryDAL GetDAL()
        {
            return STUB;
        }
    }
}

