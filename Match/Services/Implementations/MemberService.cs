﻿using Match.Dto;
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
            //var file = model.File;
            Microsoft.AspNetCore.Http.IFormFile file = model.File;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/", userId.ToString(), file.FileName);
            
            using(var db= base.NewDb())
            {
                using(var stream = System.IO.File.Create(path))
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
                    PhotoUrl = path
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

        public async Task<PageList<Member>> GetMyLikerLisk(int userId, MemberParameter para)
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

        public async Task<PageList<Member>> GetLikeMeLisk(int userId, MemberParameter para)
        {
            using (var db = base.NewDb())
            {
                var member = db.Liker
                    .Where(x => x.LikerId == userId && !x.IsDelete && !x.User.IsCloseData)
                    .Select(x => x.User)
                    .AsQueryable();

                return await PageList<Member>.CreateAsync(member, para.PageNumber, para.PageSize);
            }
        }

        public async Task<bool> DeleteMyLiker(int userId, int likeId)
        {
            using(var db = base.NewDb())
            {
                var liker = db.Liker
                    .Where(x => x.UserId == userId && x.LikerId == likeId);

                if (liker == null)
                    return false;

                db.Remove(liker);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Liker> AddMyLiker(int userId, int likeId)
        {
            using (var db = base.NewDb())
            {
                var liker = await db.Liker
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.LikerId == likeId);

                if (liker == null)
                {
                    liker = new Liker()
                    {
                        UserId = userId,
                        LikerId = likeId,
                        AddedDate = System.DateTime.Now
                    };
                    db.Add(liker);
                    await db.SaveChangesAsync();
                }
                return liker;
            }
        }

        public async Task<Message> GetMessage(int userId, int msgId)
        {
            using(var db = NewDb())
            {
                var message = await db.Message
                    .FirstOrDefaultAsync(x => x.SenderId == userId && x.Id == msgId);
                return message;
            }
        }

        public async Task<IEnumerable<Message>> GetAllMessages(int userId, MemberParameter para)
        {
            using (var db = NewDb())
            {
                var lastDate = System.DateTime.Now.AddMonths(-1);
                var messages = db.Message
                    .Include(x => x.Sender)
                    .Include(x => x.Recipient)
                    .AsQueryable();

                switch (para.MessageContainer)
                {
                    case "Inbox":
                        messages = messages
                            .Where(x => x.RecipientId == userId && x.RecipientDeleted == false && x.SendDate > lastDate);
                        break;
                    case "Outbox":
                        messages = messages
                            .Where(x => x.SenderId == userId && x.SenderDeleted == false && x.SendDate > lastDate);
                        break;
                    default:
                        messages = messages
                            .Where(x => x.RecipientId == userId && x.RecipientDeleted == false && x.SendDate > lastDate);
                        break;
                }
                messages = messages.OrderByDescending(x => x.SendDate);
                return messages;
            }
        }

        public async Task<IEnumerable<Message>> GetUnreadMessages(int userId, MemberParameter para)
        {
            using (var db = NewDb())
            {
                var lastDate = System.DateTime.Now.AddMonths(-1);
                var messages = db.Message
                    .Include(x => x.Sender)
                    .Include(x => x.Recipient)
                    .Where(x => x.RecipientId == userId && x.SendDate > lastDate && x.RecipientDeleted == false && x.IsRead == false)
                    .AsQueryable();

                messages = messages.OrderByDescending(x => x.SendDate);
                return messages;
            }
        }

        public async Task<IEnumerable<Message>> GetThreadMessages(int userId, int recipientId)
        {
            using (var db = NewDb())
            {
                var lastDate = System.DateTime.Now.AddMonths(-1);
                var messages = await db.Message
                    .Include(x => x.Sender)
                    .Include(x => x.Recipient)
                    .Where(p => p.RecipientId == userId 
                        && p.SenderId == recipientId
                        && p.RecipientDeleted == false
                        && p.SendDate > lastDate
                        || 
                        p.RecipientId == recipientId 
                        && p.SenderId == userId 
                        && p.SenderDeleted == false 
                        && p.SendDate > lastDate)
                    .OrderByDescending(x=>x.SendDate)
                    .ToListAsync();

                return messages;
            }
        }

        public async Task CreateMessage(int userId, Message model)
        {
            using(var db = NewDb())
            {
                model.SenderId = userId;
                model.SendDate = System.DateTime.Now;
                db.Add(model);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteMessage(int userId, int msgId)
        {
            using (var db = NewDb())
            {
                var message = await db.Message
                    .FirstOrDefaultAsync(x => x.Id == msgId && x.SenderId == userId);

                if (message == null)
                    return;

                if (message.SenderId == userId)
                    message.SenderDeleted = true;

                if (message.RecipientId == userId)
                    message.RecipientDeleted = true;
                
                db.Entry(message).State= EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task MakrReadMessage(int userId, int msgId)
        {
            using (var db = NewDb())
            {
                var message = await db.Message
                    .FirstOrDefaultAsync(x => x.Id == msgId && x.SenderId == userId);

                if (message == null)
                    return;

                message.IsRead = true;
                db.Entry(message).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
    }
}
