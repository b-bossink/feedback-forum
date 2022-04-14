using Data_Access;

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

        public CommentDAL GetCommentDAL()
        {
            return new CommentDAL();
        }

        public MemberDAL GetMemberDAL()
        {
            return new MemberDAL();
        }
    }
}
