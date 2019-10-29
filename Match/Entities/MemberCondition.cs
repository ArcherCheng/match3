using System;
using System.Collections.Generic;

namespace Match.Entities
{
    public partial class MemberCondition
    {
        public int UserId { get; set; }
        public int Sex { get; set; }
        public int MatchSex { get; set; }
        public int MarryMin { get; set; }
        public int MarryMax { get; set; }
        public int YearMin { get; set; }
        public int YearMax { get; set; }
        public int EducationMin { get; set; }
        public int EducationMax { get; set; }
        public int HeightsMin { get; set; }
        public int HeightsMax { get; set; }
        public int WeightsMin { get; set; }
        public int WeightsMax { get; set; }
        public int SalaryMin { get; set; }
        public string BloodInclude { get; set; }
        public string StarInclude { get; set; }
        public string CityInclude { get; set; }
        public string JobTypeInclude { get; set; }
        public string ReligionInclude { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? WriteId { get; set; }
        public string WriteIp { get; set; }

        public virtual Member User { get; set; }
    }
}
