using System;
using System.Collections.Generic;

namespace Match.Entities
{
    public partial class Liker
    {
        public int UserId { get; set; }
        public int LikerId { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? WriteId { get; set; }
        public string WriteIp { get; set; }

        public virtual Member LikerNavigation { get; set; }
        public virtual Member User { get; set; }
    }
}
