﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access.DTOs
{
    public struct CategoryDTO
    {
        public int ID;
        public string Name;
        public List<AttributeDTO> Attributes; 
    }
}
