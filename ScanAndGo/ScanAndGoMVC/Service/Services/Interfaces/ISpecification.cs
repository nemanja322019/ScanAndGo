﻿using System.Linq.Expressions;

namespace ServiceLibrary.Services.Interfaces
{
    public  interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        public Expression<Func<T, bool>> Criteriay { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
