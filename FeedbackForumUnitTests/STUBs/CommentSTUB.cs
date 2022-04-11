using System;
using Data_Access;
using Interfaces;

namespace UnitTest.STUBs
{
	public class CommentSTUB : ICommentDAL
	{

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }


        public int Update(CommentDTO comment)
        {
            throw new NotImplementedException();
        }

        public int Upload(CommentDTO comment, int parentPostID, int? parentCommentID = null)
        {
            throw new NotImplementedException();
        }
    }
}

