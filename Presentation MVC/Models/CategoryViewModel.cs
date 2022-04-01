using System;
using System.Collections.Generic;

namespace Presentation_MVC.Models
{
    public class CategoryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<AttributeViewModel> Attributes { get; set; }
    }
}

