using Match.Entities;
using Match.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Match.Services
{
    public class AuthService : ServiceBase, IAuthService
    {
        public async Task<bool> ChangePassword(Member member, string oldPassword, string newPassword)
        {
            using(var db = base.NewDb())
            {
                //先判定原密碼是否正確
                if (!PasswordHash.VerifyPasswordHash(oldPassword, member.PasswordHash, member.PasswordSalt))
                    return false;

                byte[] passwordHash, passwordSalt;
                PasswordHash.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
                member.PasswordHash = passwordHash;
                member.PasswordSalt = passwordSalt;
                member.UpdateTime = System.DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IEnumerable<GroupKeyValue>> GetGroupKeyValueList(string keyGroup)
        {
            using(var db = base.NewDb())
            {
                var list = await db.GroupKeyValue
                    .Where(x => x.KeyGroup == keyGroup)
                    .OrderBy(x => x.KeySeq)
                    .ToListAsync();
                return list;
            }
        }

        public async Task<Member> GetMember(string email, string phone)
        {
            using(var db = base.NewDb())
            {
                var result = await db.Member
                    .FirstOrDefaultAsync(x => x.Email == email && x.Phone == phone);
                return result;
            }
        }

        public async Task<Member> Login(string emailOrPhone, string password)
        {
            using(var db = base.NewDb())
            {
                var user = await db.Member
                    .FirstOrDefaultAsync(x => x.Email == emailOrPhone || x.Phone == emailOrPhone);
                if (user == null)
                    return null;

                if (!PasswordHash.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return null;

                user.LoginDate = System.DateTime.Now;
                await db.SaveChangesAsync();

                return user;
            }
        }

        public async Task<string> NewPassword(Member member)
        {
            using(var db = base.NewDb())
            {
                var newPass = new System.Random();
                var newPassword = newPass.Next(111111, 999999).ToString();

                byte[] passwordHash, passwordSalt;
                PasswordHash.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

                member.PasswordHash = passwordHash;
                member.PasswordSalt = passwordSalt;
                await db.SaveChangesAsync();

                return newPassword;
            }
        }

        public async Task<Member> Register(Member user, string password)
        {
            byte[] passwordHash, passwordSalt;
            PasswordHash.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            using(var db = base.NewDb())
            {
                db.Member.Add(user);
                await db.SaveChangesAsync();
                return user;
            }

        }

        public async Task<bool> UserIsExists(string userEmail, string userPhone)
        {
           using(var db = base.NewDb())
            {
                var user = await db.Member
                    .FirstOrDefaultAsync(p => p.Email == userEmail || p.Phone == userPhone);
                if (user == null)
                    return false;
                return true;
            }
        }

        public string UserLoginToken(Member member, string tokenSecretKey)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenSecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, member.UserId.ToString()),
                new Claim(ClaimTypes.Name,member.NickName),
                new Claim(ClaimTypes.Role,member.UserRole ?? "users" )
            };

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(30),
                SigningCredentials = creds
            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescripter);
            var tokenResult = tokenHandler.WriteToken(token);
            return tokenResult;
        }
    }
}
