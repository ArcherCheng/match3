using System;
using System.Collections.Generic;

namespace Match.Entities
{
    public partial class SysTransLog
    {
        public long Id { get; set; }
        public string TableName { get; set; }
        public string InsertData { get; set; }
        public string DeleteData { get; set; }
        public byte IudType { get; set; }
        public DateTime? WriteTime { get; set; }
    }
}
