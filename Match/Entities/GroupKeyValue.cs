using System;
using System.Collections.Generic;

namespace Match.Entities
{
    public partial class GroupKeyValue
    {
        public int Id { get; set; }
        public string KeyGroup { get; set; }
        public int KeySeq { get; set; }
        public string KeyValue { get; set; }
        public string KeyLabel { get; set; }
        public bool IsChecked { get; set; }
    }
}
