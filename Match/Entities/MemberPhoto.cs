using System;
using System.Collections.Generic;

namespace Match.Entities
{
    public partial class MemberPhoto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsMain { get; set; }
        public string Descriptions { get; set; }
        public string PhotoUrl { get; set; }
        public string PublicId { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? WriteId { get; set; }
        public string WriteIp { get; set; }

        public virtual Member User { get; set; }
    }
}
