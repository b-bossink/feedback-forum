﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackForum.Classes
{
    public class Category
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<Attribute> Attributes { get; private set; }

        public Category(string name, List<Attribute> attributes)
        {
            Name = name;
            Attributes = attributes;
        }

    }
}
