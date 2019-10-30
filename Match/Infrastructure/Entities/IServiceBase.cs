using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Infrastructure
{
    public interface IServiceBase
    {
        Task AddAsync<T>(T entity) where T : EntityBase;

        Task UpdateAsync<T>(T entity) where T : EntityBase;

        Task DeleteAsync<T>(T entity) where T : EntityBase;
    }


    public interface IServiceBase2<T> where T : EntityBase
    {
        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        T GetById(int id);

        Task<T> GetByIdAsync(int id);

        IEnumerable<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> where);

        Task<IEnumerable<T>> GetListAsync(System.Linq.Expressions.Expression<Func<T, bool>> where);

        Task<PageList<T>> GetPageListAsync(ParameterBase parameterbase, System.Linq.Expressions.Expression<Func<T, bool>> where);
    }

}
