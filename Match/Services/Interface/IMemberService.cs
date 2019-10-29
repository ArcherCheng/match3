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
        Task<MemberListDto> GetByIdAsync(int userId);
        PageResult<MemberListDto> GetUserList(PageRequest pageRequest);
        Task<PageList<Member>> GetUserPageListAsync(MemberParameter memberParameter);
    }
}
