using Interfaces;
using Interfaces.DTOs;
using Interfaces.Logic;
using System;
using System.Collections.Generic;

namespace Logic.Entities
{
    public abstract class CategoryFactory : IEntity<CategoryDTO>
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<Attribute> Attributes { get; private set; }

        public CategoryFactory(string name, List<Attribute> attributes, int id = -1)
        {
            ID = id;
            Name = name;
            Attributes = attributes;
        }

        public CategoryFactory(CategoryDTO dto)
        {
            ID = dto.ID;
            Name = dto.Name;
            Attributes = new List<Attribute>();
            foreach (AttributeDTO attributeDTO in dto.Attributes)
            {
                Attributes.Add(new Attribute(attributeDTO));
            }
        }

        public CommunicationResult Create()
        {
            int saved = GetDAL().Upload(ToDTO());
            if (saved != 1)
            {
                return CommunicationResult.UnexpectedError;
            }

            return CommunicationResult.Succes;
        }

        public CommunicationResult Update()
        {
            throw new NotImplementedException();
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

        protected abstract ICategoryDAL GetDAL();
    }
}
