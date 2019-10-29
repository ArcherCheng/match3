using Match.Entities;
using Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Dto
{
    public class MemberListDto
    {
        //不顯示 個人資料法
        public int UserId { get; set; }
        public string NickName { get; set; }
        public int BirthYear { get; set; }

        //以下為個人必要的基本條件
        public int Sex { get; set; }
        public int Marry { get; set; }
        public int Education { get; set; }

        // 以下這些資料會印在聯誼名單的個人資料上
        public int Heights { get; set; }
        public int Weights { get; set; }
        public int Salary { get; set; }
        public string Blood { get; set; }
        public string Star { get; set; }
        public string City { get; set; }
        public string JobType { get; set; }
        public string Religion { get; set; }
        public string MainPhotoUrl { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime ActiveDate { get; set; }
    }

    public static class MemberDtoExtensions
    {
        public static IQueryable<MemberListDto> ToDtos(this IQueryable<Member> query)
        {
            return from a in query
                   select new MemberListDto()
                   {
                       UserId = a.UserId,
                       NickName = a.NickName,
                       BirthYear = a.BirthYear,
                       Sex = a.Sex,
                       Marry = a.Marry
                   };
        }

        public static PageResult<MemberListDto> Build(this PageResult<MemberListDto> result, AppDbContext db)
        {
            if (result.Value.Count == 0)
            {
                return result;
            }

            return result;

        }

        public static MemberListDto ToDto(this Member source)
        {
            return new MemberListDto()
                   {
                       UserId = source.UserId,
                       NickName = source.NickName,
                       BirthYear = source.BirthYear,
                       Sex = source.Sex,
                       Marry = source.Marry
                   };
        }
    }

}
