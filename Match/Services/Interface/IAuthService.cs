using Match.Entities;
using Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Services
{
    public interface IAuthService : IServiceBase
    {
        Task<Member> Register(Member user, string password);
        Task<Member> Login(string userEmail, string password);
        Task<bool> UserIsExists(string emailOrPhone, string userPhone);
        Task<Member> GetMember(string email, string phone);
        Task<IEnumerable<GroupKeyValue>> GetGroupKeyValueList(string keyGroup);
        Task<string> NewPassword(Member member);
        Task<bool> ChangePassword(Member member, string oldPassword, string newPassword);
        string UserLoginToken(Member member,string tokenSecretKey);
    }
}
