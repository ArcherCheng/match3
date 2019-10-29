using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Infrastructure
{
    public interface IEntityBase
    {
        //int Id { get; set; }

        DateTime? CreateTime { get; set; }

        DateTime? UpdateTime { get; set; }

        //int? WriteId { get; set; }

        //string WriteIp { get; set; }   
    }
}
