using System;
using Interfaces;
using Interfaces.DTOs;
using Logic.Containers;
using Logic.Entities;
using UnitTest.STUBs;
using UnitTest.TestEntities;

namespace UnitTest.TestContainers
{
    public class TestPostContainer : PostContainerFactory
    {
        public PostSTUB STUB { get; private set; }
        public TestPostContainer(PostSTUB stub)
        {
            STUB = stub;
        }
        protected override PostFactory CreatePost(PostDTO dto)
        {
            return new TestPost(dto);
        }

        protected override IPostDAL GetDAL()
        {
            return STUB;
        }
    }
}

