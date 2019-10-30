using Match.Entities;
using Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Dto
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Member, MemberListDto>().ReverseMap();

            CreateMap<RegisterDto, Member>();

            CreateMap<Member, UserToReturnDto>();

            CreateMap<Member, MemberEditDto>()
            .ForMember(dest => dest.Introduction, opt => opt.MapFrom(src => src.MemberDetail.Introduction))
            .ForMember(dest => dest.LikeCondition, opt => opt.MapFrom(src => src.MemberDetail.LikeCondition))
            .ReverseMap();
        }
    }
}
