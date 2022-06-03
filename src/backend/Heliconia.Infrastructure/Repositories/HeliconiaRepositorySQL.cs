using Heliconia.Domain;
using Heliconia.Domain.CategoryEntities;
using Heliconia.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Infrastructure.Repositories
{
    public class HeliconiaRepositorySQL : IRepository
    {
        protected readonly HeliconiaContext context;

        public HeliconiaRepositorySQL(HeliconiaContext context)
        {
            this.context = context;
        }

        public void Delete<T>(T obj) where T : Entity
        {
            try
            {
                context.Set<T>().Remove(obj);
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public bool Exists<T>(Expression<Func<T, bool>> expression) where T : Entity
        {
            try
            {
                return context.Set<T>()
                    .AsQueryable()
                    .Where(x => x.IsRemoved == false)
                    .Any(expression);
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public bool Exists<T>(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> conditionWhereOne, Expression<Func<T, bool>> conditionWhereSecond) where T : Entity
        {
            try
            {
                return context.Set<T>()
                    .AsQueryable()
                    .Where(x => x.IsRemoved == false)
                    .Where(conditionWhereOne)
                    .Where(conditionWhereSecond)
                    .Any(expression);
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public bool Exists<T>(Expression<Func<T, bool>> conditionWhereOne, Expression<Func<T, bool>> expression) where T : Entity
        {
            try
            {
                return context.Set<T>()
                    .AsQueryable()
                    .Where(x => x.IsRemoved == false)
                    .Where(conditionWhereOne)
                    .Any(expression);
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> expression) where T : Entity
        {
            try
            {
                return await context.Set<T>()
                    .Where(x => x.IsRemoved == false)
                    .FirstOrDefaultAsync(expression);
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> conditionWhereOne , Expression<Func<T, bool>> expression) where T : Entity
        {
            try
            {
                return await context.Set<T>()
                    .Where(x => x.IsRemoved == false)
                    .Where(conditionWhereOne)
                    .FirstOrDefaultAsync(expression);
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<List<T>> GetAll<T>(Expression<Func<T, string>> sort, int page, int pageSize,
            Expression<Func<T, bool>> expressionConditional) where T : Entity
        {
            try
            {
                int skipRows = page * pageSize;
                return await context.Set<T>()
                    .Where(x => x.IsRemoved == false)
                    .Where(expressionConditional)
                    .OrderBy(sort)
                    .Skip(skipRows)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<List<T>> GetAll<T>(Expression<Func<T, bool>> expressionConditional) where T : Entity
        {
            try
            {
                return await context.Set<T>()
                    .Where(x => x.IsRemoved == false)
                    .Where(expressionConditional)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<List<T>> GetAllNested<T>(Expression<Func<T, bool>> expressionConditional, string nested) where T : Entity
        {
            try
            {
                return await context.Set<T>()
                   .Where(x => x.IsRemoved == false)
                   .Where(expressionConditional)
                   .Include(nested)
                   .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }

        }

        public async Task<T> GetLastNested<T>(Expression<Func<T, DateTime>> sort, Expression<Func<T, bool>> expressionConditional, string nested) where T : Entity
        {
            try
            {
                return await context.Set<T>()
                   .Where(x => x.IsRemoved == false)
                   .Where(expressionConditional)
                   .OrderByDescending(sort)
                   .Include(nested)
                   .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }

        }

        public async Task Save<T>(T obj) where T : Entity
        {
            try
            {
                await context.Set<T>().AddAsync(obj);
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task Commit()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public void Update<T>(T obj) where T : Entity
        {
            try
            {
                context.Update<T>(obj);
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<int> Count<T>(Expression<Func<T, bool>> expressionConditional) where T : Entity
        {
            try
            {
                return await context.Set<T>().CountAsync(expressionConditional);
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<T> GetNested<T>(Expression<Func<T, bool>> expression, string nested) where T : Entity
        {
            try
            {
                return await context.Set<T>()
                    .Where(x => x.IsRemoved == false)
                    .Where(expression)
                    .Include(nested)
                    .FirstOrDefaultAsync();
            }

            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<List<T>> GetAll<T>(Expression<Func<T, string>> sort, int page, int pageSize, Expression<Func<T, bool>> conditionWhereOne, Expression<Func<T, bool>> conditionWhereSecond) where T : Entity
        {
            try
            {
                int skipRows = page * pageSize;
                return await context.Set<T>()
                    .Where(x => x.IsRemoved == false)
                    .Where(conditionWhereOne)
                    .Where(conditionWhereSecond)
                    .OrderBy(sort)
                    .Skip(skipRows)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }


        public async Task<List<T>> GetAll<T>(Expression<Func<T, bool>> expressionFirst,
            Expression<Func<T, bool>> expressionSecond) where T : Entity
        {
            try
            {
                return await context.Set<T>()
                    .Where(x => x.IsRemoved == false)
                    .Where(expressionFirst)
                    .Where(expressionSecond)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<List<T>> GetAll<T>(int page, int pageSize, Expression<Func<T, bool>> condition) where T : Entity
        {
            try
            {
                int skipRows = page * pageSize;
                return await context.Set<T>()
                    .Where(x => x.IsRemoved == false)
                    .Where(condition)
                    .Skip(skipRows)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

    }
}
