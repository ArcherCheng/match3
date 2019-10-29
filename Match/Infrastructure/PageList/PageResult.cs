using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Infrastructure
{
    public class PageResult
    {
        public int PageCurrent { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPage
        {
            get { return PageCurrent > 0; }
        }

        public bool HasNextPage
        {
            get { return PageCurrent + 1 < TotalPages; }
        }

        public void UpdateFrom(PageResult result)
        {
            this.PageCurrent = result.PageCurrent;
            this.PageSize = result.PageSize;
            this.TotalCount = result.TotalCount;
            this.TotalPages = result.TotalPages;
        }
    }

    public class PageResult<T> : PageResult
    {
        public List<T> Value { get; set; }
        public PageResult()
        {
            this.Value = new List<T>();
        }
        public PageResult(IQueryable<T> source, int pageCurrent, int pageSize) 
            : this()
        {
            if (pageSize == 0)
            {
                pageSize=10;
            }
            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = (int)System.Math.Ceiling(TotalCount / (double)pageSize);
            this.PageSize = pageSize;
            this.PageCurrent = pageCurrent;
            this.Value.AddRange(source.Skip(pageCurrent * pageSize).Take(pageSize).ToList());
        }

        public PageResult(IList<T> source, int pageCurrent, int pageSize) 
            : this()
        {
            if (pageSize == 0)
            {
                return;
            }
            int total = source.Count;
            this.TotalCount = total;
            this.TotalPages = (int)System.Math.Ceiling(TotalCount / (double)pageSize);
            this.PageSize = pageSize;
            this.PageCurrent = pageCurrent;
            this.Value.AddRange(source.Skip(pageCurrent * pageSize).Take(pageSize).ToList());
        }
        public PageResult(IEnumerable<T> source, int pageCurrent, int pageSize, int totalCount) 
            : this()
        {
            if (pageSize == 0)
            {
                return;
            }
            //int total = source.Count();
            this.TotalCount = totalCount;
            this.TotalPages = (int)System.Math.Ceiling(TotalCount / (double)pageSize);
            this.PageSize = pageSize;
            this.PageCurrent = pageCurrent;
            this.Value.AddRange(source.Skip(pageCurrent * pageSize).Take(pageSize).ToList());
        }
    }
}
