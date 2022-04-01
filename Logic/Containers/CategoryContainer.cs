using Data_Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Containers
{
    public class CategoryContainer
    {
        private CategoryDAL DAL = new CategoryDAL();

        public CategoryContainer()
        {
        }

        public List<Category> GetAll()
        {
            List<Category> result = new List<Category>();
            foreach (CategoryDTO dto in DAL.LoadAll())
            {
                result.Add(new Category(dto));
            }
            return result;
        }

        public Category Get(int id)
        {
            foreach (CategoryDTO dto in DAL.LoadAll())
            {
                if (id == dto.ID)
                {
                    return new Category(dto);
                }
            }
            return null;
        }
    }
}
