using Interfaces;
using Interfaces.DTOs;
using Interfaces.Logic;
using Logic.Entities;

namespace Logic.Containers
{
    public abstract class MemberContainerFactory : IContainer<MemberDTO>
    {
        /// <returns>The member based on the given credentials. Null when credentials are invalid.</returns>
        public IEntity<MemberDTO> Get(string username, string password)
        {
            MemberDTO? dto = GetDAL().Get(username, password);
            if (dto == null)
                return null;

            return new Member((MemberDTO)dto);
        }

        /// <returns>The member with the given ID. Null when ID is not found.</returns>
        public IEntity<MemberDTO> Get(int id)
        {
            MemberDTO? dto = GetDAL().Get(id);
            if (dto == null)
                return null;

            return new Member((MemberDTO)dto);
        }

        public IEntity<MemberDTO>[] GetAll()
        {
            throw new System.NotImplementedException();
        }

        public CommunicationResult Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        protected abstract IMemberDAL GetDAL();
        protected abstract MemberFactory CreateMember(MemberDTO dto);
    }
}
