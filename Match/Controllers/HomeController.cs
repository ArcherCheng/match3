using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Match.Dto;
using Match.Entities;
using Match.Infrastructure;
using Match.Services;
using Match.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Match.Controllers
{

    public class HomeController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMemberService _service;

        public HomeController(IMapper mapper, IMemberService service)
        {
            this._mapper = mapper;
            this._service = service;
        }

        [HttpGet("userList")]
        public async Task<IActionResult> GetUserPageList([FromQuery] MemberParameter parameters)
        {
            var service = Ioc.Get<IMemberService>();
            var memberList = await _service.GetUserPageListAsync(parameters);
            base.AddPaginationHeader(memberList);
            var memberListDto = _mapper.Map<IEnumerable<MemberListDto>>(memberList);
            return Ok(memberListDto);
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
            var member = await _service.GetByIdAsync(userId);
            
            return Ok(member);
        }

        [HttpGet("userDetail/{userId}")]
        public async Task<IActionResult> GetUserDetail(int userId)
        {
            var service = Ioc.Get<IMemberService>();
            var detail =await _service.GetUserDetail(userId);
            return Ok(detail);
        }

        [HttpGet("userPhotos/{userId}")]
        public async Task<IActionResult> GetUserPhotos(int userId)
        {
            var service = Ioc.Get<IMemberService>();
            var userPhotos = await _service.GetUserPhotos(userId);
            return Ok(userPhotos);
        }

        [HttpGet("userCondition/{userId}")]
        public async Task<IActionResult> GetUserCondition(int userId)
        {
            var service = Ioc.Get<IMemberService>();
            MemberCondition memberCondition = await service.GetUserCondition(userId);
            if(memberCondition == null)
            {
                memberCondition = new MemberCondition();
                memberCondition.UserId = userId;
                memberCondition.BloodInclude = "";
                memberCondition.StarInclude = "";
                memberCondition.CityInclude = "";
                memberCondition.JobTypeInclude = "";
                memberCondition.ReligionInclude = "";
            }
            return Ok(memberCondition);
        }

        //未登入或註冊者用
        [HttpPost("userMatchList")]
        public async Task<IActionResult> GetMatchList([FromBody]MemberCondition condition,[FromQuery]MemberParameter memberParameter)
        {
            memberParameter.Condition = condition;
            memberParameter.UserId = 0;

            var service = Ioc.Get<IMemberService>();
            PageList<Member> list = await service.GetMatchList(memberParameter);
            return Ok(list);
        }

        //已經登入者用
        [Authorize]
        [HttpPost("userMatchList/{userId}")]
        public async Task<IActionResult> GetMatchList(int userId, [FromQuery]MemberParameter memberParameter)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            memberParameter.UserId = userId;

            var service = Ioc.Get<IMemberService>();
            PageList<Member> list = await service.GetMatchList(memberParameter);
            return Ok(list);
        }

        //已經登入者用
        [Authorize]
        [HttpPost("userCondition/update/{userId}")]
        public async Task<IActionResult> UserConditionUpdate(int userId, [FromBody]MemberCondition condition)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var service = Ioc.Get<IMemberService>();

            await service.UpdateCondition(userId,condition);
 
            return Ok(condition);
        }



    }
}