using Interfaces;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;

namespace UnitTest.STUBs
{
    public class PostSTUB : IPostDAL
    {
        public List<PostDTO> database;

        public PostSTUB()
        {
            List<AttributeDTO> attributes = new List<AttributeDTO>()
            {
                new AttributeDTO()
                {
                    ID = 1,
                    Name = "Attribute 1"
                },
                new AttributeDTO()
                {
                    ID = 2,
                    Name = "Attribute 2"
                }
            };

            Dictionary<AttributeDTO, string> attributeDict = new Dictionary<AttributeDTO, string>
            {
                { attributes[0], "My first value!" },
                { attributes[1], "Another value!!" }
            };

            MemberDTO member = new MemberDTO
            {
                ID = 11,
                Username = "gebruiker",
                Emailaddress = "gebruiker@email.nl",
                Password = "password"
            };

            List<PostDTO> result = new List<PostDTO>()
            {
                new PostDTO
                {
                    ID = 123,
                    Name = "My Test Post",
                    Comments = new List<CommentDTO>()
                    {
                        new CommentDTO
                        {
                            ID = 1,
                            CreationDate = Convert.ToDateTime("3-12-2022"),
                            Replies = new List<CommentDTO>(),
                            Text = "This is a test comment.",
                            Upvotes = 12,
                            Owner = member
                        }
                    },
                    Category = new CategoryDTO()
                    {
                        ID = 1,
                        Name = "Test Category",
                        Attributes = attributes
                    },
                    CreationDate = Convert.ToDateTime("2-11-2022"),
                    Upvotes = 52,
                    ValuesByAttributes = attributeDict,
                    Owner = member
                }
            };

            database = result;
        }

        public List<PostDTO> LoadAll()
        {
            return database;
        }

        public int Upload(PostDTO postDTO)
        {
            int before = database.Count;
            database.Add(postDTO);
            int after = database.Count;
            return after - before;
        }

        public int Update(PostDTO postDTO)
        {
            int result = 0;
            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].ID == postDTO.ID)
                {
                    database[i] = postDTO;
                    result++;
                }
            }
            return result;
        }

        public int Delete(int id)
        {
            int before = database.Count;

            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].ID == id)
                {
                    _ = database.Remove(database[i]);
                }
            }
            int after = database.Count;
            int result = before - after;
            return result;
        }

        public bool Exists(int id)
        {
            foreach (PostDTO dto in database)
            {
                if (dto.ID == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
