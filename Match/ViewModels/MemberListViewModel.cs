using Match.Dto;
using Match.Infrastructure;
using Match.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Match.ViewModels
{
    public class MemberListViewModel
    {
        public PageResult<MemberListDto> MemberListDto { get; set; }

        private readonly PageRequest _pageRequest;

        public MemberListViewModel(PageRequest pageRequest)
        {
            this._pageRequest = pageRequest; 
        }

        public MemberListViewModel Build()
        {
            var service = Ioc.Get<IMemberService>();
            MemberListDto = service.GetUserList(_pageRequest);
            return this;
        }
    }


}
