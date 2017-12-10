using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    public class ConcernInfo
    {
        public Guid id { get; set; }
        public Guid myid { get; set; }   // 用户id
        public Guid toid { get; set; }   // 对象id
    }
}
