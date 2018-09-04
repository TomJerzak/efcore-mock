using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EFCore.Mock
{
    internal class EfCoreAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public EfCoreAsyncEnumerable(Expression expression) : base(expression)
        {
        }

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new EfCoreAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider => new EfCoreAsyncQueryProvider<T>(this);
    }
}
