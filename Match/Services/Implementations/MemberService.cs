using Match.Dto;
using Match.Entities;
using Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.IO;

namespace Match.Services
{
    public class MemberService : ServiceBase, IMemberService 
    {
        public async Task<Member> GetByIdAsync(int userId) 
        {
            using (var db = base.NewDb())
            {
                var result = await db.Member.FindAsync(userId);
                return result;
            }
        }

        public async Task<PageList<Member>> GetUserPageListAsync(MemberParameter parameters)
        {
            using (var db = base.NewDb())
            {
                var list = db.Member
                    .Where(x => !x.IsCloseData)
                    .OrderByDescending(x => x.LoginDate)
                    .AsQueryable();

                return await PageList<Member>.CreateAsync(list, parameters.PageNumber, parameters.PageSize);
            }
        }

        public PageResult<MemberListDto> GetUserList(PageRequest pageRequest)
        {
            using (var db = base.NewDb())
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

        public async Task<MemberDetail> GetUserDetail(int userId)
        {
            using(var db = base.NewDb())
            {
                var result = await db.MemberDetail
                    .FindAsync(userId);
                return result;
            }
        }

        public async Task<IEnumerable<MemberPhoto>> GetUserPhotos(int userId)
        {
            using (var db = base.NewDb())
            {
                var list = await db.MemberPhoto
                    .Where(x => x.UserId == userId)
                    .ToListAsync();

                return list;
            }
        }

        public async Task<MemberCondition> GetUserCondition(int userId)
        {
            using (var db = base.NewDb())
            {
                var result = await db.MemberCondition
                    .FindAsync(userId);
                return result;
            }
        }

        public async Task<PageList<Member>> GetMatchList(MemberParameter parameter)
        {
            using (var db = base.NewDb())
            {
                var memberCondition = db.MemberCondition.FirstOrDefault(x => x.UserId == parameter.UserId);
                if(memberCondition == null)
                {
                    memberCondition = parameter.Condition;
                }

                var matchList = db.Member
                    .Where(x => x.Sex == memberCondition.MatchSex)
                    .OrderByDescending(x => x.LoginDate)
                    .AsQueryable();

                if (memberCondition.MarryMin > 0 && memberCondition.MarryMax > 0)
                {
                    matchList = matchList.Where(x => (x.Marry >= memberCondition.MarryMin && x.Marry <= memberCondition.MarryMax));
                }

                if (memberCondition.YearMin > 0 && memberCondition.YearMax > 0)
                {
                    matchList = matchList.Where(x => (x.BirthYear >= memberCondition.YearMin && x.BirthYear <= memberCondition.YearMax));
                }

                if (memberCondition.EducationMin > 0 && memberCondition.EducationMax > 0)
                {
                    matchList = matchList.Where(x => (x.Education >= memberCondition.EducationMin && x.Education <= memberCondition.EducationMax));
                }

                if (memberCondition.HeightsMin > 0 && memberCondition.HeightsMax > 0)
                {
                    matchList = matchList.Where(x => (x.Heights >= memberCondition.HeightsMin && x.Heights <= memberCondition.HeightsMax));
                }

                if (memberCondition.WeightsMin > 0 && memberCondition.WeightsMax > 0)
                {
                    matchList = matchList.Where(x => (x.Weights >= memberCondition.WeightsMin && x.Weights <= memberCondition.WeightsMax));
                }

                if (!string.IsNullOrEmpty(memberCondition.BloodInclude))
                {
                    matchList = matchList.Where(x => memberCondition.BloodInclude.Contains(x.Blood));
                }

                if (!string.IsNullOrEmpty(memberCondition.StarInclude))
                {
                    matchList = matchList.Where(x => memberCondition.StarInclude.Contains(x.Star));
                }

                if (!string.IsNullOrEmpty(memberCondition.CityInclude))
                {
                    matchList = matchList.Where(x => memberCondition.CityInclude.Contains(x.City));
                }

                if (!string.IsNullOrEmpty(memberCondition.JobTypeInclude))
                {
                    matchList = matchList.Where(x => memberCondition.JobTypeInclude.Contains(x.JobType));
                }

                if (!string.IsNullOrEmpty(memberCondition.ReligionInclude))
                {
                    matchList = matchList.Where(x => memberCondition.ReligionInclude.Contains(x.Religion));
                }

                return await PageList<Member>.CreateAsync(matchList, parameter.PageNumber, parameter.PageSize);
            }
        }

        public async Task UpdateCondition(int userId,MemberCondition condition)
        {
            using (var db = base.NewDb())
            {
                if (userId == 0)
                {
                    condition.UserId = userId;
                    //db.Set<MemberCondition>().Add(condition);
                    db.Add(condition);
                    await db.SaveChangesAsync();
                }
                else
                {
                    db.Entry(condition).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task<MemberPhoto> GetPhoto(int photoId)
        {
            using(var db = base.NewDb())
            {
                var photo = await db.MemberPhoto.FindAsync(photoId);
                return photo;
            }
        }

        public async Task UploadPhoto(int userId, PhotoCreateDto model)
        {
            //Microsoft.AspNetCore.Http.IFormFile files = model.File;
            var file = model.File;

            using(var db= base.NewDb())
            {

                using(var stream = System.IO.File.Create(file.FileName))
                {
                    await file.CopyToAsync(stream);
                }

                bool isMain = true;
                if (await HasMainPhoto(userId))
                    isMain = false;


                var memberPhoto = new MemberPhoto()
                {
                    UserId = userId,
                    AddedDate = System.DateTime.Now,
                    Descriptions = model.descriptions?? file.FileName,
                    IsMain = isMain ,
                    PhotoUrl = file.FileName
                };

                db.Add(memberPhoto);

                await db.SaveChangesAsync();
                return;
            }
        }

        public async Task<bool> HasMainPhoto(int userId)
        {
            using(var db = base.NewDb())
            {
                var photo = await db.MemberPhoto
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.IsMain);

                if (photo == null)
                    return false;

                return true;
            }
        }

        public async Task<MemberPhoto> SetMainPhoto(int userId, int photoId)
        {
            using (var db = base.NewDb())
            {
                var photos = db.MemberPhoto
                    .Where(x => x.UserId == userId && x.IsMain);

                if(photos != null)
                {
                    foreach (var phote in photos)
                    {
                        phote.IsMain = false;
                        db.Entry(phote).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    }
                }

                var mainPhoto = await db.MemberPhoto
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == photoId);

                mainPhoto.IsMain = true;
                db.Entry(mainPhoto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await db.SaveChangesAsync();

                return mainPhoto;
            }
        }

        public async Task DeletePhoto(int userId, int photoId)
        {
            using (var db = base.NewDb())
            {
                var phote = await db.MemberPhoto
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == photoId);

                if (phote.IsMain)
                {
                    var mainPhoto = await db.MemberPhoto.FirstOrDefaultAsync(x => x.UserId == userId);
                    mainPhoto.IsMain = true;
                    db.Entry(mainPhoto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                db.Remove(phote);
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateMember(MemberEditDto model)
        {
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var mapper = services.GetService<IMapper>();
            using(var db = base.NewDb())
            {
                var member =await db.Member.FindAsync(model.UserId);
                mapper.Map(model, member);
                if (member.MemberDetail == null)
                {
                    var memberDetail = new MemberDetail()
                    {
                        UserId = model.UserId,
                        Introduction = model.Introduction,
                        LikeCondition = model.LikeCondition
                    };
                    member.MemberDetail = memberDetail;
                }
                else
                {
                    member.MemberDetail.Introduction = model.Introduction;
                    member.MemberDetail.LikeCondition = model.LikeCondition;
                }

                db.Entry(member).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task<PageList<Member>> GetMylikerLisk(int userId, MemberParameter para)
        {
            using(var db = base.NewDb())
            {
                var member = db.Liker
                    .Where(x => x.UserId == userId && !x.IsDelete && !x.LikerNavigation.IsCloseData)
                    .Select(x => x.LikerNavigation)
                    .AsQueryable();

                return await PageList<Member>.CreateAsync(member, para.PageNumber, para.PageSize);
            }
        }
    }
}
