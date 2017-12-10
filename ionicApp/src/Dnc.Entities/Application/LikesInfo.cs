using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    public class LikesInfo
    {
        public Guid peid { get; set; }   // 用户id
        public Guid toid { get; set; }   // 对象id
    }
}
