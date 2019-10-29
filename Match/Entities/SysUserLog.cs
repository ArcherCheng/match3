using System;
using System.Collections.Generic;

namespace Match.Entities
{
    public partial class SysUserLog
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Refer { get; set; }
        public string Destination { get; set; }
        public string QueryString { get; set; }
        public string Method { get; set; }
        public string IpAddress { get; set; }
        public DateTime? RequestTime { get; set; }
    }
}
