using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    public class MessageInfo
    {

        public string Content { get; set; }            //留言内容

        public Guid Personid { get; set; }  
        public Guid Dynamicid { get; set; } 
    }
}
