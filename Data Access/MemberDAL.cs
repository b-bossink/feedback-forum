﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Interfaces;
using Interfaces.DTOs;

namespace Data_Access
{
    public class MemberDAL : MSSQLConnection, IMemberDAL
    {

        public MemberDTO? Get(string username, string password)
        {
            string query = "SELECT * FROM [User] WHERE Username = @Username AND Password = @Password";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(new SqlParameter("@Username", username));
            cmd.Parameters.Add(new SqlParameter("@Password", password));

            MemberDTO member = new MemberDTO();

            if (username != null && password != null)
            {
                if (OpenConnection())
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            member = new MemberDTO
                            {
                                ID = (int)reader["id"],
                                Username = (string)reader["username"],
                                Emailaddress = (string)reader["email"],
                                Password = (string)reader["password"]
                            };
                        }
                    }
                    CloseConnection();
                }
            }

            if (member.ID < 1)
                return null;

            return member;
        }

        public MemberDTO? Get(int id)
        {
            string query = "SELECT * FROM [User] WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(new SqlParameter("@ID", id));

            MemberDTO member = new MemberDTO();

            if (OpenConnection())
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        member = new MemberDTO
                        {
                            ID = id,
                            Username = (string)reader["username"],
                            Emailaddress = (string)reader["email"],
                            Password = (string)reader["password"]
                        };
                    }
                }
            }

            if (member.Username == null)
                return null;

            return member;

        }

        public int Upload(MemberDTO member)
        {
            if (!OpenConnection())
                return 0;

            if (UsernameExists(member.Username) || EmailExists(member.Emailaddress))
            {
                throw new DuplicateNameException("Email or username already exists");
            }

            if (!OpenConnection())
                return 0;

            string query = "insert into [User] (username, password, email) values " +
                $"(@Username, @Password, @Email);";
            SqlCommand cmd = new SqlCommand(query, connection);
            ConnectionState state = connection.State;
            cmd.Parameters.Add(new SqlParameter("@Username",member.Username));
            cmd.Parameters.Add(new SqlParameter("@Password", member.Password));
            cmd.Parameters.Add(new SqlParameter("@Email", member.Emailaddress));
            int savedRows = cmd.ExecuteNonQuery();
            CloseConnection();
            return savedRows;
        }

        public bool UsernameExists(string username)
        {
            string query = "SELECT COUNT (*) FROM [User] WHERE username = @Username";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(new SqlParameter("@Username", username));

            OpenConnection();
            int result = (int)cmd.ExecuteScalar();
            CloseConnection();
            return result > 0;
        }

        public bool EmailExists(string email) {
            string query = "SELECT COUNT (*) FROM [User] WHERE email = @Email";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(new SqlParameter("@Email", email));

            OpenConnection();
            int result = (int)cmd.ExecuteScalar();
            CloseConnection();
            return result > 0;
        }

        public int Update(MemberDTO member)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<MemberDTO> LoadAll()
        {
            throw new NotImplementedException();
        }
    }
}
