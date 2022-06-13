using Data_Access;
using Interfaces;
using Interfaces.DTOs;

namespace Logic.Factories
{
    public class MemberDALCreator : DALFactory<MemberDTO>
    {
        public override IDAL<MemberDTO> GetDAL()
        {
            return new MemberDAL();
        }
    }
}

