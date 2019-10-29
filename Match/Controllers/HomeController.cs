using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Match.Entities;
using Match.Infrastructure;
using Match.Services;
using Match.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Match.Controllers
{

    public class HomeController : ApiControllerBase
    {
        [HttpGet("userList")]
        public async Task<IActionResult> GetUserPageList([FromQuery] MemberParameter parameters)
        {
            var service = Ioc.Get<IMemberService>();
            var pageList = await service.GetUserPageListAsync(parameters);
            //Response.AddPagination(pageList.PageCurrent, pageList.PageSize, pageList.TotalCount, pageList.TotalPages);
            base.AddPaginationHeader(pageList);
            return Ok(pageList);
        }

        [HttpGet("userList2")]
        public IActionResult GetUserPageResult([FromQuery] PageRequest parameters)
        {
            var model = new MemberListViewModel(parameters).Build();
            return Ok(model);
        }

        [HttpGet("userData/{userId}")]
        public async Task<IActionResult> GetUserData(int userId)
        {
            var service = Ioc.Get<IMemberService>();
            var memberDto = await service.GetByIdAsync(userId);
            
            return Ok(memberDto);
        }

    }
}