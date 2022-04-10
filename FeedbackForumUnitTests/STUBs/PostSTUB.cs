﻿using Data_Access;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.STUBs
{
    class PostSTUB : IPostDAL
    {
        private List<PostDTO> database;

        public PostSTUB()
        {
            database = LoadAll();
        }

        public List<PostDTO> LoadAll()
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

            Dictionary<AttributeDTO, string> attributeDict = new Dictionary<AttributeDTO, string>();
            attributeDict.Add(attributes[1], "My first value!");
            attributeDict.Add(attributes[2], "Another value!!");

            List<PostDTO> result = new List<PostDTO>()
            {
                new PostDTO
                {
                    ID = 1,
                    Name = "My Test Post",
                    Comments = new List<CommentDTO>()
                    {
                        new CommentDTO
                        {
                            ID = 1,
                            CreationDate = Convert.ToDateTime("3-12-2022"),
                            Replies = new List<CommentDTO>(),
                            Text = "This is a test comment.",
                            Upvotes = 12
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
                    ValuesByAttributes = attributeDict
                }
            };

            return result;
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
            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].ID == postDTO.ID)
                {
                    database[i] = postDTO;
                    return 1;
                }
            }
            return 0;
        }

        public int Delete(int id)
        {
            int before = database.Count;

            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].ID == id)
                {
                    database.Remove(database[i]);
                }
            }
            int after = database.Count;
            return before - after;
        }
    }
}
