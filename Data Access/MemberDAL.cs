using System;
using Data_Access.DTOs;
using Data_Access.Interfaces;
using System.Data.SqlClient;

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
            throw new NotImplementedException();
        }

        public MemberDTO Get(int id)
        {
            string query = "SELECT * FROM [User] WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(new SqlParameter("@ID", id));

            MemberDTO member = new MemberDTO();

            OpenConnection();
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
            CloseConnection();
            return member;

        }

        public void Update(MemberDTO member)
        {
            throw new NotImplementedException();
        }
    }
}
