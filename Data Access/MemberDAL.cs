using System;
using System.Data.SqlClient;
using Interfaces;
using Interfaces.DTOs;

namespace Data_Access
{
    public class MemberDAL : MSSQLConnection, IMemberDAL
    {
        public void Add(MemberDTO member)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public MemberDTO Get(string username, string password)
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

            if (member.ID > 0)
                return member;

            throw new System.Security.Authentication.InvalidCredentialException();
        }

        public MemberDTO Get(int id)
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
            //CloseConnection();
            return member;

        }

        public void Update(MemberDTO member)
        {
            throw new NotImplementedException();
        }
    }
}
