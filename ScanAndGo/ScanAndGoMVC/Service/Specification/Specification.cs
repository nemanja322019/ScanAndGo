using ServiceLibrary.Services.Interfaces;
using System.Linq.Expressions;

namespace ServiceLibrary.Specification
{
    public class Specification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria => throw new NotImplementedException();

        public List<Expression<Func<T, object>>> Includes => throw new NotImplementedException();

        public Expression<Func<T, bool>> Criteriay => throw new NotImplementedException();

        public Expression<Func<T, object>> OrderBy => throw new NotImplementedException();

        public Expression<Func<T, object>> OrderByDescending => throw new NotImplementedException();

        public int Take => throw new NotImplementedException();

        public int Skip => throw new NotImplementedException();

        public bool IsPagingEnabled => throw new NotImplementedException();
    }
}
