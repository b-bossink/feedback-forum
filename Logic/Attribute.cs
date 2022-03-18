using Data_Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Attribute
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public Attribute(string name, int id = -1)
        {
            ID = id;
            Name = name;
        }

        public AttributeDTO ToDTO()
        {
            return new AttributeDTO
            {
                ID = this.ID,
                Name = this.Name
            };
        }
    }
}
