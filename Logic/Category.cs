using Data_Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Category
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<Attribute> Attributes { get; private set; }

        public Category(string name, List<Attribute> attributes, int id = -1)
        {
            ID = id;
            Name = name;
            Attributes = attributes;
        }

        public Category(CategoryDTO dto)
        {
            Name = dto.Name;
            Attributes = new List<Attribute>();
            foreach (AttributeDTO attributeDTO in dto.Attributes)
            {
                Attributes.Add(new Attribute(attributeDTO));
            }
        }
        public bool Upload()
        {
            return new CategoryDAL().Upload(ToDTO());
        }

        public CategoryDTO ToDTO()
        {
            List<AttributeDTO> attributes = new List<AttributeDTO>();
            foreach(Attribute attribute in Attributes)
            {
                attributes.Add(attribute.ToDTO());
            }

            return new CategoryDTO
            {
                ID = this.ID,
                Name = this.Name,
                Attributes = attributes
            };
        }

    }
}
