using System;
using System.Collections.Generic;
using Data_Access;
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
        private IMemberDAL DAL;

        public Member(IMemberDAL dal, MemberDTO dto)
        {
            DAL = dal;
            ID = dto.ID;
            Username = dto.Username;
            Emailaddress = dto.Emailaddress;
            Password = dto.Password;
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        private MemberDTO ToDTO()
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
