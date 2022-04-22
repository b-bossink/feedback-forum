using Data_Access;
using Interfaces;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Containers
{
    public class CategoryContainer
    {
        private ICategoryDAL DAL;

        public CategoryContainer(ICategoryDAL dal)
        {
            DAL = dal;
        }

        public List<Category> GetAll()
        {
            List<Category> result = new List<Category>();
            foreach (CategoryDTO dto in DAL.LoadAll())
            {
                result.Add(new Category(new CategoryDAL(), dto));
            }
            return result;
        }

        public Category Get(int id)
        {
            foreach (CategoryDTO dto in DAL.LoadAll())
            {
                if (id == dto.ID)
                {
                    return new Category(new CategoryDAL(), dto);
                }
            }
            return null;
        }
    }
}
