﻿using Data_Access;

namespace Logic.Factories
{
    public class DALFactory
    {
        public PostDAL GetPostDAL()
        {
            return new PostDAL();
        }

        public CategoryDAL GetCategoryDAL()
        {
            return new CategoryDAL();
        }
    }
}