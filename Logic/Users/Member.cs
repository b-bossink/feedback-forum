using System;
using System.Collections.Generic;
using Interfaces;
using Interfaces.DTOs;

namespace Logic.Users
{
    public class Member
    {
        public int ID { get; private set; }
        public string Username { get; private set; }
        public string Emailaddress { get; private set; }
        public string Password { get; private set; }
        private readonly IMemberDAL _DAL;

        public Member(IMemberDAL dal, string username, string email, string password, int id = -1)
        {
            _DAL = dal;
            ID = id;
            Username = username;
            Emailaddress = email;
            Password = password;
        }

        public Member(MemberDTO dto)
        {
            ID = dto.ID;
            Username = dto.Username;
            Emailaddress = dto.Emailaddress;
            Password = dto.Password;
        }

        public CommunicationResult Register()
        {
            if (_DAL.UsernameExists(Username))
            {
                return CommunicationResult.DuplicateUsernameError;
            }

            if (_DAL.EmailExists(Emailaddress))
            {
                return CommunicationResult.DuplicateEmailError;
            }

            int rowsSaved = _DAL.Upload(ToDTO());
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
    }
}
