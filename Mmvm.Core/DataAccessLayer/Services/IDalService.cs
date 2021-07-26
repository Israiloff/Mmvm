using System;
using System.Collections.Generic;
using System.Linq.Expressions;

// ReSharper disable IdentifierTypo

namespace Israiloff.Mmvm.Net.Core.DataAccessLayer.Services
{
    public interface IDalService<TDto> : IDisposable where TDto : class
    {
        TDto Insert(TDto entity);

        IEnumerable<TDto> InsertAll(IEnumerable<TDto> dtos);

        TDto Update(TDto entity);

        IEnumerable<TDto> UpdateAll(IEnumerable<TDto> dtos);

        void Delete(TDto entity);

        void DeleteAll();

        void DeleteAll(IEnumerable<TDto> entities);

        TDto Find(long id);

        TDto FindFirst(Expression<Func<TDto, bool>> predicate);

        TDto FindLast(Expression<Func<TDto, bool>> predicate);

        IEnumerable<TDto> FindAll();

        IEnumerable<TDto> FindAll(Expression<Func<TDto, bool>> predicate);

        long Count();

        long Count(Expression<Func<TDto, bool>> predicate);
    }
}