using System;
using Data_Access;
using Interfaces;
using Interfaces.DTOs;

namespace Logic.Factories
{
    public class CategoryDALCreator : DALFactory<CategoryDTO>
    {
        public override IDAL<CategoryDTO> GetDAL()
        {
            return new CategoryDAL();
        }
    }
}

