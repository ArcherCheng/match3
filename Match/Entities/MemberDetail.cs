using System;
using System.Collections.Generic;

namespace Match.Entities
{
    public partial class MemberDetail
    {
        public int UserId { get; set; }
        public string Introduction { get; set; }
        public string LikeCondition { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? WriteId { get; set; }
        public string WriteIp { get; set; }

        public virtual Member User { get; set; }
    }
}
