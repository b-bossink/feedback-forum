using System;
using Data_Access;
using Interfaces;
using Interfaces.DTOs;

namespace Logic.Factories
{
	public class CommentDALCreator : DALFactory<CommentDTO>
	{
        public override IDAL<CommentDTO> GetDAL()
        {
            return new CommentDAL();
        }
    }
}

