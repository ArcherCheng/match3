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
        public async Task<IActionResult> GetPhoto(int photoId)
        {
            var photo = await _service.GetPhoto(photoId);
            return Ok(photo);
        }

        [HttpPost("uploadPhotos")]
        public async Task<IActionResult> UploadPhotos(int userId, [FromForm]PhotoCreateDto model)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var member = await _service.GetByIdAsync(userId);
            if (member == null)
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

        [HttpPost("myLikerList")]
        public async Task<IActionResult> GetMylikerLisk(int userId, [FromQuery]MemberParameter para)
        {
            if (userId != int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            PageList<Member> members = await _service.GetMylikerLisk(userId, para);
            base.AddPaginationHeader(members);
            var memberListDto = _mapper.Map<IEnumerable<MemberListDto>>(members);
            return Ok(memberListDto);
        }





    }
}