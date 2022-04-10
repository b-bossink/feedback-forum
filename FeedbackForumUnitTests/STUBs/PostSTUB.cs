using Data_Access;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.STUBs
{
    class PostSTUB : IPostDAL
    {
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<PostDTO> LoadAll()
        {
            throw new NotImplementedException();
        }

        public int Update(PostDTO postDTO)
        {
            throw new NotImplementedException();
        }

        public int Upload(PostDTO postDTO)
        {
            throw new NotImplementedException();
        }
    }
}
