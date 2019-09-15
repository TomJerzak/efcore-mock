using System.Collections.Generic;
using System.Linq;
using EFCore.Mock.Example.Application;
using EFCore.Mock.Example.Application.Entities;
using EFCore.Mock.Example.Application.Exceptions;
using EFCore.Mock.Example.Application.Repositories;
using EFCore.Mock.Example.Application.Services;
using EFCore.Mock.Example.Tests.Services.Data;
using FluentAssertions;
using Xunit;
using Moq;

namespace EFCore.Mock.Example.Tests.Services
{
    public class BlogServiceTests
    {
        [Fact]
        public void get_blogs()
        {
            var mockDbContext = new Mock<DatabaseContext>();
            ICrudRepository<Blog> repository = new BlogService(mockDbContext.Object);
            mockDbContext.Setup(p => p.Blogs).Returns(EfCoreService.GetDbSet(BlogData.GetAll()).Object);

            repository.GetAll().Count().Should().Be(1);
        }

        [Fact]
        public void get_blog_by_id()
        {
            var mockDbContext = new Mock<DatabaseContext>();
            ICrudRepository<Blog> repository = new BlogService(mockDbContext.Object);
            mockDbContext.Setup(p => p.Blogs).Returns(EfCoreService.GetDbSet(BlogData.GetAll()).Object);

            repository.GetById(1).Url.Should().Be("https://www.fake.blog.com");
        }

        [Fact]
        public void throw_exception_for_get_on_no_exist_blog_by_id()
        {
            var mockDbContext = new Mock<DatabaseContext>();
            ICrudRepository<Blog> repository = new BlogService(mockDbContext.Object);
            mockDbContext.Setup(p => p.Blogs).Returns(EfCoreService.GetDbSet(BlogData.GetAll()).Object);

            var exception = Record.Exception(() => repository.GetById(2));

            exception.Should().NotBeNull();
            exception.Should().BeAssignableTo<EntityNotFoundException>();
        }

        [Fact]
        public void throw_exception_for_create_on_exist_blog()
        {
            var mockDbContext = new Mock<DatabaseContext>();
            ICrudRepository<Blog> repository = new BlogService(mockDbContext.Object);

            var blog = new Blog()
            {
                Url = "https://www.fake.blog.com"
            };
            mockDbContext.Setup(x => x.Blogs).Returns(EfCoreService.GetDbSet(new List<Blog> {blog}).Object);

            var exception = Record.Exception(() => repository.Create(blog));

            exception.Should().NotBeNull();
            exception.Should().BeAssignableTo<EntityAlreadyExistException>();
        }

        [Fact]
        public void update_blog()
        {
            var mockDbContext = new Mock<DatabaseContext>();
            ICrudRepository<Blog> repository = new BlogService(mockDbContext.Object);

            var blog = new Blog()
            {
                BlogId = 1,
                Url = "https://www.test.blog.com"
            };
            mockDbContext.Setup(x => x.Blogs).Returns(EfCoreService.GetDbSet(new List<Blog> {blog}).Object);

            var result = repository.Update(blog);

            result.Should().NotBeNull();
            result.Url.Should().Be("https://www.test.blog.com");
        }

        [Fact]
        public void throw_exception_for_update_on_no_exist_blog()
        {
            var mockDbContext = new Mock<DatabaseContext>();
            ICrudRepository<Blog> repository = new BlogService(mockDbContext.Object);

            var blog = new Blog()
            {
                BlogId = 1,
                Url = "https://www.test.blog.com"
            };
            mockDbContext.Setup(x => x.Blogs).Returns(EfCoreService.GetDbSet(new List<Blog>()).Object);

            var exception = Record.Exception(() => repository.Update(blog));

            exception.Should().NotBeNull();
            exception.Should().BeAssignableTo<EntityNotFoundException>();
        }

        [Fact]
        public void delete_blog()
        {
            var mockDbContext = new Mock<DatabaseContext>();
            ICrudRepository<Blog> repository = new BlogService(mockDbContext.Object);

            var blog = new Blog()
            {
                BlogId = 1,
                Url = "https://www.test.blog.com"
            };
            mockDbContext.Setup(x => x.Blogs).Returns(EfCoreService.GetDbSet(new List<Blog> { blog }).Object);

            repository.Delete(1).Should().NotBeNull();
        }

        [Fact]
        public void throw_exception_for_delete_on_no_exist_blog()
        {
            var mockDbContext = new Mock<DatabaseContext>();
            ICrudRepository<Blog> repository = new BlogService(mockDbContext.Object);

            mockDbContext.Setup(x => x.Blogs).Returns(EfCoreService.GetDbSet(new List<Blog>()).Object);

            var exception = Record.Exception(() => repository.Delete(1));

            exception.Should().NotBeNull();
            exception.Should().BeAssignableTo<EntityNotFoundException>();
        }
    }
}