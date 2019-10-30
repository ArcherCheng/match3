using Match.Dto;
using Match.Entities;
using Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Services
{
    public interface IMemberService : IServiceBase
    {
        Task<Member> GetByIdAsync(int userId);
        PageResult<MemberListDto> GetUserList(PageRequest pageRequest);
        Task<PageList<Member>> GetUserPageListAsync(MemberParameter memberParameter);
        Task<MemberDetail> GetUserDetail(int userId);
        Task<IEnumerable<MemberPhoto>> GetUserPhotos(int userId);
        Task<MemberCondition> GetUserCondition(int userId);
        Task<PageList<Member>> GetMatchList(MemberParameter memberParameter);
        Task UpdateCondition(int userId, MemberCondition condition);
        Task<MemberPhoto> GetPhoto(int photoId);
        Task UploadPhoto(int userId, PhotoCreateDto model);
        Task<bool> HasMainPhoto(int userId);
        Task<MemberPhoto> SetMainPhoto(int userId, int photoId);
        Task DeletePhoto(int userId, int photoId);
        Task UpdateMember(MemberEditDto model);
        Task<PageList<Member>> GetMylikerLisk(int userId, MemberParameter para);
    }
}
