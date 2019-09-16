# efcore-mock (EFCore.Mock)

Mock for Entity Framework Core.

## Install [EFCore.Mock](https://www.nuget.org/packages/EFCore.Mock)

* Package Manager

```bash
Install-Package EFCore.Mock -Version 1.0.0
```

* .NET CLI

```bash
dotnet add package EFCore.Mock --version 1.0.0
```

* PackageReference

```xml
<PackageReference Include="EFCore.Mock" Version="1.0.0" />
```

* Paket CLI

```bash
paket add EFCore.Mock --version 1.0.0
```

## Examples

* [Application (EFCore.Mock.Example.Application)](https://github.com/TomJerzak/efcore-mock/tree/master/samples/EFCore.Mock.Example.Application)
* [Unit tests (EFCore.Mock.Example.Tests)](https://github.com/TomJerzak/efcore-mock/tree/master/samples/EFCore.Mock.Example.Tests)

## Example of usage GetDbSet method

* BlogData class

```c#
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
```

* Unit test

```c#
[Fact]
public void get_blog_by_id()
{
    var mockDbContext = new Mock<DatabaseContext>();
    ICrudRepository<Blog> repository = new BlogService(mockDbContext.Object);
    mockDbContext.Setup(p => p.Blogs).Returns(EfCoreService.GetDbSet(BlogData.GetAll()).Object);

    repository.GetById(1).Url.Should().Be("https://www.fake.blog.com");
}
```
