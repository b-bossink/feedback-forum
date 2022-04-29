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
            List<CategoryDTO> dtos = DAL.LoadAll();
            if (dtos != null)
            {
                List<Category> result = new List<Category>();
                foreach (CategoryDTO dto in dtos)
                {
                    result.Add(new Category(new CategoryDAL(), dto));
                }
                return result;
            }
            return null;

        }

        public Category Get(int id)
        {
            List<CategoryDTO> dtos = DAL.LoadAll();
            if (dtos != null)
            {
                foreach (CategoryDTO dto in dtos)
                {
                    if (id == dto.ID)
                    {
                        return new Category(new CategoryDAL(), dto);
                    }
                }
            }
            return null;
        }
    }
}
