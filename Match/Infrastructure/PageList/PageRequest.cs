using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Infrastructure
{
    public class PageRequest
    {
        public int PageCurrent { get; set; }

        private int pageSize;

        public int PageSize 
        { 
            get { return pageSize; }
            set 
            { 
                if(value > 100)
                {
                    pageSize = 100;
                }
                else if(value==0)
                {
                    pageSize = 10;
                }
                else
                {
                    pageSize = value;
                }

            }
        }

        public string Sort { get; set; }

        public SortDirection SortDirection { get; set; }
    }

    public enum SortDirection
    {
        None = 0,
        Asc = 1,
        Desc = 2
    }
}
