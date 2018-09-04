using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace EFCore.Mock
{
    public static class EfCoreService
    {
        public static Mock<DbSet<T>> GetDbSet<T>(ICollection<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.AsQueryable().GetEnumerator());

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new EfCoreAsyncQueryProvider<T>(entities.AsQueryable().Provider));
            mockSet.As<IAsyncEnumerable<T>>().Setup(m => m.GetEnumerator()).Returns(new EfCoreAsyncEnumerator<T>(entities.GetEnumerator()));

            return mockSet;
        }
    }
}
