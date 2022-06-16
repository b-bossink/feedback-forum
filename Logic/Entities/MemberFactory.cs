using System;
using Interfaces.Logic;
using Interfaces.DTOs;
using Data_Access;
using Interfaces;

namespace Logic.Entities
{
    public abstract class MemberFactory : IEntity<MemberDTO>
    {
        public int ID { get; private set; }
        public string Username { get; private set; }
        public string Emailaddress { get; private set; }
        public string Password { get; private set; }

        public MemberFactory(string username, string email, string password, int id = -1)
        {
            ID = id;
            Username = username;
            Emailaddress = email;
            Password = password;
        }

        public MemberFactory(MemberDTO dto)
        {
            ID = dto.ID;
            Username = dto.Username;
            Emailaddress = dto.Emailaddress;
            Password = dto.Password;
        }

        public CommunicationResult Create()
        {
            if (GetDAL().UsernameExists(Username))
            {
                return CommunicationResult.DuplicateUsernameError;
            }

            if (GetDAL().EmailExists(Emailaddress))
            {
                return CommunicationResult.DuplicateEmailError;
            }

            int rowsSaved = GetDAL().Upload(ToDTO());
            if (rowsSaved != 1)
            {
                return CommunicationResult.UnexpectedError;
            }

            return CommunicationResult.Succes;
        }

        public MemberDTO ToDTO()
        {
            return new MemberDTO
            {
                ID = this.ID,
                Username = this.Username,
                Emailaddress = this.Emailaddress,
                Password = this.Password
            };
        }

        public CommunicationResult Update()
        {
            throw new NotImplementedException();
        }

        protected abstract IMemberDAL GetDAL();
    }
}
