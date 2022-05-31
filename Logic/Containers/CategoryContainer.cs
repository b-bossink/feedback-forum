using Interfaces;
using Interfaces.DTOs;
using System.Collections.Generic;

namespace Logic.Containers
{
    public class CategoryContainer
    {
        private readonly ICategoryDAL _DAL;

        public CategoryContainer(ICategoryDAL dal)
        {
            _DAL = dal;
        }

        public List<Category> GetAll()
        {
            List<CategoryDTO> dtos = _DAL.LoadAll();
            if (dtos != null)
            {
                List<Category> result = new List<Category>();
                foreach (CategoryDTO dto in dtos)
                {
                    result.Add(new Category(dto));
                }
                return result;
            }
            return null;

        }

        /// <returns>The category with the given ID. Null when ID is not found.</returns>
        public Category Get(int id)
        {
            List<CategoryDTO> dtos = _DAL.LoadAll();
            if (dtos != null)
            {
                foreach (CategoryDTO dto in dtos)
                {
                    if (id == dto.ID)
                    {
                        return new Category(dto);
                    }
                }
            }
            return null;
        }
    }
}
