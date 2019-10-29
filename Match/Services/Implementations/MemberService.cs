using Match.Dto;
using Match.Entities;
using Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Match.Services
{
    public class MemberService : ServiceBase, IMemberService
    {
        //var mapper = configuration.CreateMapper();
        public async Task<MemberListDto> GetByIdAsync(int userId)
        {
            using (var db = NewDb())
            {
                var member = await db.Member.FindAsync(userId);
                return member.ToDto();
            }
        }

        public PageResult<MemberListDto> GetUserList(PageRequest pageRequest)
        {
            using (var db = NewDb())
            {
                var list = db.Member
                    .Where(x => !x.IsCloseData)
                    .OrderByDescending(x => x.LoginDate)
                    .ToDtos()
                    .ToPageResult(pageRequest)
                    .Build(db);

                return list;
            }
        }

        public async Task<PageList<Member>> GetUserPageListAsync(MemberParameter parameters)
        {
            using (var db = NewDb())
            {
                var list = db.Member
                    .Where(x => !x.IsCloseData)
                    .OrderByDescending(x => x.LoginDate)
                    .AsQueryable();

                return await PageList<Member>.CreateAsync(list, parameters.PageNumber, parameters.PageSize);
            }
        }
    }
}
