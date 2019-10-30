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
        Task<PageList<Member>> GetMyLikerLisk(int userId, MemberParameter para);
        Task<PageList<Member>> GetLikeMeLisk(int userId, MemberParameter para);
        Task<bool> DeleteMyLiker(int userId, int likeId);
        Task<Liker> AddMyLiker(int userId, int likeId);
        Task<Message> GetMessage(int userId, int msgId);
        Task<IEnumerable<Message>> GetAllMessages(int userId, MemberParameter para);
        Task<IEnumerable<Message>> GetUnreadMessages(int userId, MemberParameter para);
        Task<IEnumerable<Message>> GetThreadMessages(int userId, int recipientId);
        Task CreateMessage(int userId, Message model);
        Task DeleteMessage(int userId, int msgId);
        Task MakrReadMessage(int userId, int msgId);
    }
}
