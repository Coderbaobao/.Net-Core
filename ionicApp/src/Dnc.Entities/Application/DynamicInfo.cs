using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    public class DynamicInfo
    {
        public Guid PersonID { get; set; }   // 用户id
        public string Content { get; set; }   // 正文内容

        public string FileName { get; set; }   // 图片
    }
}
