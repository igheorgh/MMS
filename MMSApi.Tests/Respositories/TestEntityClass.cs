﻿namespace MMSApi.Tests.Respositories
{
    public class TestEntityClass
    {
        public TestEntityClass(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
