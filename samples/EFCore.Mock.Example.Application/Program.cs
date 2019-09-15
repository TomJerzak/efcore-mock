using System;
using EFCore.Mock.Example.Application.Entities;
using EFCore.Mock.Example.Application.Repositories;
using EFCore.Mock.Example.Application.Services;

namespace EFCore.Mock.Example.Application
{
    class Program
    {
        private static readonly DatabaseContext DatabaseContext = new DatabaseContext();
        private static readonly ICrudRepository<Blog> BlogRepository = new BlogService(DatabaseContext);

        static void Main(string[] args)
        {
            Console.WriteLine("EFCore.Mock.Example.Application\n");
            WriteSeparator();

            CreateBlogs();
            PrintAllBlogs();
            WriteSeparator();

            UpdateBlog();
            DeleteBlogs();
            PrintAllBlogs();
        }

        private static void PrintAllBlogs()
        {
            foreach (var blog in BlogRepository.GetAll())
                Console.WriteLine($"Id: {blog.BlogId}, Url: {blog.Url}");
        }

        private static void WriteSeparator()
        {
            Console.WriteLine(new String('-', 40));
        }

        private static Blog CreateNewBlog(string url)
        {
            return new Blog {Url = url};
        }

        private static void UpdateBlog()
        {
            var blogById = BlogRepository.GetById(1);
            blogById.Url = "https;//www.fake.blog.com";
            BlogRepository.Update(blogById);
        }
        
        private static void CreateBlogs()
        {
            BlogRepository.Create(CreateNewBlog("https;//www.fake1.blog.com"));
            BlogRepository.Create(CreateNewBlog("https;//www.fake2.blog.com"));
            BlogRepository.Create(CreateNewBlog("https;//www.fake3.blog.com"));
        }

        private static void DeleteBlogs()
        {
            BlogRepository.Delete(2);
            BlogRepository.Delete(3);
        }
    }
}