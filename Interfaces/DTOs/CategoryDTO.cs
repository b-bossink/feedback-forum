using System.Collections.Generic;

namespace Interfaces.DTOs
{
    public class CategoryDTO : DTO
    {
        public string Name { get; set; }
        public List<AttributeDTO> Attributes { get; set; }
    }
}
