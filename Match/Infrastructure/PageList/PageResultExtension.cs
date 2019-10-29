using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Infrastructure
{
    public static class PageResultExtension
    {
        public static PageResult<T> ToPageResult<T>(this IEnumerable<T> source,int pageCurrent,int pageSize)
        {
            return new PageResult<T>(source, pageCurrent, pageSize, source.Count());
        }

        public static PageResult<T> ToPageResult<T>(this IQueryable<T> linq, int pageCurrent, int pageSize)
        {
            return new PageResult<T>(linq, pageCurrent, pageSize);
        }

        public static PageResult<T> ToPageResult<T>(this IQueryable<T> query, PageRequest request)
        {
            return new PageResult<T>(query.OrderBy(request.Sort, request.SortDirection), request.PageCurrent, request.PageSize);
        }

        public static PageResult<T> ToPageResult<T>(this IList<T> source, int pageCurrent, int pageSize)
        {
            return new PageResult<T>(source, pageCurrent, pageSize);
        }

    }
}
