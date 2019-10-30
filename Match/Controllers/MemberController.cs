using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Match.Dto;
using Match.Entities;
using Match.Infrastructure;
using Match.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Match.Controllers
{
    [Authorize]
    [Route("api/[controller]/{userId}")]
    public class MemberController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMemberService _service;

        public MemberController(IMapper mapper, IMemberService service)
        {
            this._mapper = mapper;
            this._service = service;
        }

        [HttpGet("editMember")]
        public async Task<IActionResult> EditMember(int userId)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var member = await _service.GetByIdAsync(userId);
            return Ok(member);
        }

        [HttpGet("getPhotos")]
        public async Task<IActionResult> GetPhotos(int userId)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var photos = await _service.GetUserPhotos(userId);
            return Ok(photos);
        }

        [HttpGet("getPhoto/{photoId}")]
        public async Task<IActionResult> GetPhoto(int userId, int photoId)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var photo = await _service.GetPhoto(photoId);
            return Ok(photo);
        }

        [HttpPost("uploadPhotos")]
        public async Task<IActionResult> UploadPhotos(int userId, [FromForm]PhotoCreateDto model)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var file = model.File;
            if (file == null || file.Length == 0)
                return BadRequest("未選取檔案,無法上傳");

            await _service.UploadPhoto(userId, model);
            return Ok();
        }

        [HttpPost("setMainPhoto")]
        public async Task<IActionResult> SetMainPhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var photo = await _service.SetMainPhoto(userId, id);
            if (photo == null)
                return BadRequest("設定相片封面失敗");

            return Ok("設定相片封面成功");
        }

        [HttpPost("deletePhoto")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            await _service.DeletePhoto(userId, id);
            return Ok("刪除相片成功");
        }

        [HttpPost("updateMember")]
        public async Task<IActionResult> UpdateMember(int userId, MemberEditDto model)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            await _service.UpdateMember(model);
            return Ok();
        }

        [HttpGet("myLikerList")]
        public async Task<IActionResult> GetMylikerLisk(int userId, [FromQuery]MemberParameter para)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            PageList<Member> members = await _service.GetMyLikerLisk(userId, para);
            base.AddPaginationHeader(members);

            var memberListDto = _mapper.Map<IEnumerable<MemberListDto>>(members);
            return Ok(memberListDto);
        }

        [HttpDelete("deleteMyLiker/{likeId}")]
        public async Task<IActionResult> DeleteMyliker(int userId, int likeId)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            bool success = await _service.DeleteMyLiker(userId, likeId);
            if (!success)
                return BadRequest("取消好友失敗");

            // return NoContent();
            // return Ok( "成功加入我的最愛" );
            return Ok();
        }

        [HttpPost("addMyLiker/{likeId}")]
        public async Task<IActionResult> AddMyliker(int userId, int likeId)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var liker = await _service.AddMyLiker(userId, likeId);
            if (liker == null)
                return BadRequest("加入好友失敗");

            return Ok(liker);
        }

        [HttpPost("getMessage/{msgId}")]
        public async Task<IActionResult> GetMessage(int userId, int msgId)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var message = await _service.GetMessage(userId, msgId);
            return Ok(message);
        }

        [HttpGet("getAllMessages")]
        public async Task<IActionResult> GetAllMessages(int userId,[FromQuery]MemberParameter para)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messages = await _service.GetAllMessages(userId, para);
            return Ok(messages);
        }

        [HttpGet("getUnreadMessages")]
        public async Task<IActionResult> GetUnreadMessages(int userId, [FromQuery]MemberParameter para)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messages = await _service.GetUnreadMessages(userId, para);
            return Ok(messages);
        }

        [HttpGet("threadMessage/{recipientId}")]
        public async Task<IActionResult> GetThreadMessage(int userId, int recipientId)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messages = await _service.GetThreadMessages(userId, recipientId);
            return Ok(messages);
        }

        [HttpPost("createMessage")]
        public async Task<IActionResult> CreateMessage(int userId, [FromBody]Message model)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            await _service.CreateMessage(userId, model);

            return Ok();
        }

        [HttpPost("deleteMessage/{msgId}")]
        public async Task<IActionResult> DeleteMessage(int userId,int msgId)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            await _service.DeleteMessage(userId, msgId);

            return Ok();
        }

        [HttpPost("markRead/{msgId}")]
        public async Task<IActionResult> MarkMessageAsRead(int userId, int msgId)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            await _service.MakrReadMessage(userId, msgId);

            return Ok();
        }

    }
}