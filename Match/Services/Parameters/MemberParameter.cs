using Match.Entities;
using Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Services
{
    public class MemberParameter : ParameterBase
    {
        public int UserId { get; set; } //= 0;
        public MemberCondition Condition { get; set; }
        public string MessageContainer { get; set; }
    }
}
