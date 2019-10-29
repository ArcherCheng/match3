using System;
using System.Collections.Generic;

namespace Match.Entities
{
    public partial class Member
    {
        public Member()
        {
            LikerLikerNavigation = new HashSet<Liker>();
            LikerUser = new HashSet<Liker>();
            MemberPhoto = new HashSet<MemberPhoto>();
            MessageRecipient = new HashSet<Message>();
            MessageSender = new HashSet<Message>();
        }

        public int UserId { get; set; }
        public string NickName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Sex { get; set; }
        public int BirthYear { get; set; }
        public int Marry { get; set; }
        public int Education { get; set; }
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
        public bool IsCloseData { get; set; }
        public bool IsClosePhoto { get; set; }
        public string MainPhotoUrl { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime? LoginDate { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string UserRole { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? WriteId { get; set; }
        public string WriteIp { get; set; }

        public virtual MemberCondition MemberCondition { get; set; }
        public virtual MemberDetail MemberDetail { get; set; }
        public virtual ICollection<Liker> LikerLikerNavigation { get; set; }
        public virtual ICollection<Liker> LikerUser { get; set; }
        public virtual ICollection<MemberPhoto> MemberPhoto { get; set; }
        public virtual ICollection<Message> MessageRecipient { get; set; }
        public virtual ICollection<Message> MessageSender { get; set; }
    }
}
