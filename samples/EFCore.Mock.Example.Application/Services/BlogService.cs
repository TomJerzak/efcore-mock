using System.Linq;
using EFCore.Mock.Example.Application.Entities;
using EFCore.Mock.Example.Application.Exceptions;
using EFCore.Mock.Example.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Mock.Example.Application.Services
{
    public class BlogService : ICrudRepository<Blog>
    {
        private readonly DatabaseContext _databaseContext;

        public BlogService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IQueryable<Blog> GetAll()
        {
            return _databaseContext.Blogs
                .AsNoTracking();
        }

        public Blog GetById(long id)
        {
            var entity = GetById(id, useAsNoTracking: true);
            if (entity == null)
                throw new EntityNotFoundException(nameof(Blog) + " id: " + id);

            return entity;
        }

        private Blog GetById(long id, bool useAsNoTracking)
        {
            Blog entity;

            if (useAsNoTracking)
                entity = _databaseContext.Blogs
                    .AsNoTracking()
                    .FirstOrDefault(p => p.BlogId == id);
            else
                entity = _databaseContext.Blogs
                    .FirstOrDefault(p => p.BlogId == id);

            if (entity == null)
                throw new EntityNotFoundException(nameof(Blog) + " id: " + id);

            return entity;
        }

        public bool IsExist(string url)
        {
            var entity = _databaseContext.Blogs
                .AsNoTracking()
                .FirstOrDefault(p => p.Url.ToLower().Equals(url.ToLower()));

            return entity != null;
        }

        public Blog Create(Blog item)
        {
            if (IsExist(item.Url))
                throw new EntityAlreadyExistException(nameof(Blog) + " name: " + item.Url);

            item.Url = item.Url;
            _databaseContext.Blogs.Add(item);

            _databaseContext.SaveChanges();
            return item;
        }

        public Blog Update(Blog item)
        {
            var entity = GetById(item.BlogId, useAsNoTracking: false);
            if (entity == null)
                throw new EntityNotFoundException(nameof(Blog) + " id: " + item.BlogId);

            entity.Url = item.Url;

            _databaseContext.Update(entity);
            _databaseContext.SaveChanges();

            return item;
        }

        public Blog Delete(long id)
        {
            var entity = GetById(id, useAsNoTracking: false);
            if (entity == null)
                throw new EntityNotFoundException(nameof(Blog) + " id: " + id);

            _databaseContext.Remove(entity);
            _databaseContext.SaveChanges();

            return entity;
        }
    }
}