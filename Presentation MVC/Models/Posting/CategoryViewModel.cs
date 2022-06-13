using System.Collections.Generic;

namespace Presentation_MVC.Models.Posting
{
    public class CategoryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<AttributeViewModel> Attributes { get; set; }
    }
}

