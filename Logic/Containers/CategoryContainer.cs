using System;
using Data_Access;
using Interfaces;
using Interfaces.DTOs;
using Logic.Entities;

namespace Logic.Containers
{
    public class CategoryContainer : CategoryContainerFactory
    {
        protected override CategoryFactory CreateCategory(CategoryDTO dto)
        {
            return new Category(dto);
        }

        protected override ICategoryDAL GetDAL()
        {
            return new CategoryDAL();
        }
    }
}

