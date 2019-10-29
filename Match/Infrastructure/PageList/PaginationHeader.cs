using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Infrastructure
{
    public class PaginationHeader
    {
        public int PageCurrent { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public PaginationHeader(int pageCurrent, int pageSize, int totalCount, int totalPages)
        {
            this.PageCurrent = pageCurrent;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            this.TotalPages = totalPages;
        }
    }
    public static class PaginationHeaderExtensions
    {
        public static void AddPagination(this HttpResponse response,
            int pageCurrent, int pageSize, int totalCount, int totalPages)
        {
            var paginationHeader = new PaginationHeader(pageCurrent, pageSize, totalCount, totalPages);
            //var camelCaseFormatter = new JsonSerializerSettings();
            //camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var camelCaseFormatter = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
 
        }
    }
}
