using System.Collections.Generic;

namespace Interfaces.DTOs
{
    public struct CategoryDTO
    {
        public int ID;
        public string Name;
        public List<AttributeDTO> Attributes; 
    }
}
