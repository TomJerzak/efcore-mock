using System.Collections.Generic;
using EFCore.Mock.Example.Application.Entities;

namespace EFCore.Mock.Example.Tests.Services.Data
{
    internal static class BlogData
    {
        public static List<Blog> GetAll()
        {
            var list = new List<Blog>
            {
                new Blog()
                {
                    BlogId = 1,
                    Url = "https://www.fake.blog.com"
                }
            };

            return list;
        }
    }
}