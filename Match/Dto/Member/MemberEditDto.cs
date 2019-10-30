using Match.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Dto
{
    public class MemberEditDto
    {
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int BirthYear { get; set; }
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
        public string School { get; set; }
        public string Subjects { get; set; }
        public string JobType { get; set; }
        public string Religion { get; set; }

        //以下為個人的其他未顯示欄位
        public bool IsCloseData { get; set; }
        public bool IsClosePhoto { get; set; }
        public string MainPhotoUrl { get; set; }
        public string LikeCondition { get; set; }
        public string Introduction { get; set; }
        public virtual MemberDetail MemberDetail { get; set; }
    }
}
