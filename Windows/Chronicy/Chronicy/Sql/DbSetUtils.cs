using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Chronicy.Sql
{
    public static class DbSetUtils
    {
        public static IEnumerable<T> Find<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate) where T : class
        {
            var local = dbSet.Local.Where(predicate.Compile());
            return local.Any()
                ? local
                : dbSet.Where(predicate).ToArray();
        }

        public static async Task<IEnumerable<T>> FindAsync<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate) where T : class
        {
            var local = dbSet.Local.Where(predicate.Compile());
            return local.Any()
                ? local
                : await dbSet.Where(predicate).ToArrayAsync().ConfigureAwait(false);
        }
    }
}
