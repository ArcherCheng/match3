using AutoMapper;
using Match.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Match.Infrastructure
{
    public class ServiceBase : IServiceBase
    {
        protected AppDbContext NewDb()
        {
            return new AppDbContext();
        }

        public async Task AddAsync<T>(T entity) where T : EntityBase
        {
            using (var db = new AppDbContext())
            {
                //db.Add(entity);
                db.Set<T>().Add(entity);
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync<T>(T entity) where T : EntityBase
        {
            using (var db = new AppDbContext())
            {
                //db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //db.SaveChanges();

                var entry = db.Entry(entity);
                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                {
                    db.Set<T>().Attach(entity);
                }
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync<T>(T entity) where T : EntityBase
        {
            using (var db = new AppDbContext())
            {
                ////db.Remove(entity);
                //db.Set<EntityBase>().Remove(entity);
                //db.SaveChanges();

                var entry = db.Entry(entity);
                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                {
                    db.Set<T>().Attach(entity);
                }
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                db.Set<EntityBase>().Remove(entity);
                await db.SaveChangesAsync();
            }
        }
    }



    public class ServiceBase2<T> : IServiceBase2<T>  where T : EntityBase
    {
        protected AppDbContext NewDb()
        {
            return new AppDbContext();
        }

        public async Task AddAsync(T entity) 
        {
            using(var db= new AppDbContext())
            {
                //db.Add(entity);
                db.Set<T>().Add(entity);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            using (var db = new AppDbContext())
            {
                ////db.Remove(entity);
                //db.Set<T>().Remove(entity);
                //db.SaveChanges();

                var entry = db.Entry(entity);
                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                {
                    db.Set<T>().Attach(entity);
                }
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                db.Set<T>().Remove(entity);
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(T entity)
        {
            using (var db = new AppDbContext())
            {
                //db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //db.SaveChanges();

                var entry = db.Entry(entity);
                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                {
                    db.Set<T>().Attach(entity);
                }
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public T GetById(int id)
        {
            using(var db = new AppDbContext())
            {
                //var Set = db.Set<T>();
                //var TResult = Set.Find(id);
                //return TResult;
                return db.Set<T>().Find(id);
            }
        }
        public async Task<T> GetByIdAsync(int id)
        {
            using (var db = new AppDbContext())
            {
                return await db.Set<T>().FindAsync(id);
            }
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>> where)
        {
            using (var db = new AppDbContext())
            {
                var result = db.Set<T>().Where(where);
                return result;
            }
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> where)
        {
            using (var db = new AppDbContext())
            {
                var result = db.Set<T>().Where(where);
                return await result.ToListAsync();
            }
        }

        public Task<PageList<T>> GetPageListAsync(ParameterBase parameterbase, Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }
    }
}
