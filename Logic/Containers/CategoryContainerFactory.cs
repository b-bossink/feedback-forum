using Interfaces;
using Interfaces.DTOs;
using Interfaces.Logic;
using Logic.Entities;
using System.Collections.Generic;

namespace Logic.Containers
{
    public abstract class CategoryContainerFactory : IContainer<CategoryDTO>
    {
        public IEntity<CategoryDTO>[] GetAll()
        {
            List<CategoryDTO> dtos = GetDAL().LoadAll();
            if (dtos != null)
            {
                List<CategoryFactory> result = new List<CategoryFactory>();
                foreach (CategoryDTO dto in dtos)
                {
                    result.Add(new Category(dto));
                }
                return result.ToArray();
            }
            return null;

        }

        /// <returns>The category with the given ID. Null when ID is not found.</returns>
        public IEntity<CategoryDTO> Get(int id)
        {
            List<CategoryDTO> dtos = GetDAL().LoadAll();
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

        public CommunicationResult Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        protected abstract ICategoryDAL GetDAL();
        protected abstract CategoryFactory CreateCategory(CategoryDTO dto);
    }
}
