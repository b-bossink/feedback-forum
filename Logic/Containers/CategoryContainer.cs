using Data_Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Containers
{
    public class CategoryContainer
    {
        public List<Category> Categories { get; private set; }
        private CategoryDAL DAL = new CategoryDAL();

        public CategoryContainer()
        {
            Categories = new List<Category>();
            Refresh();
        }

        private Category ToCategory(CategoryDTO dto)
        {
            List<Attribute> attributes = new List<Attribute>();
            foreach (AttributeDTO attributeDTO in dto.Attributes)
            {
                attributes.Add(ToAttribute(attributeDTO));
            }
            return new Category(
                dto.Name,
                attributes
                );
        }

        private Attribute ToAttribute(AttributeDTO dto)
        {
            return new Attribute(
                dto.Name,
                dto.ID
                );
        }

        public void Refresh()
        {
            Categories.Clear();
            foreach (CategoryDTO dto in DAL.LoadAll())
            {
                Categories.Add(ToCategory(dto));
            }
        }
    }
}
