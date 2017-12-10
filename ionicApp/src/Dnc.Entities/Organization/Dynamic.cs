using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Organization
{
    public class Dynamic : IEntity
    {
        [Key]
        public Guid ID { get; set; }

        public string Content { get; set; }   // 正文内容
        [StringLength(255)]
        public string Image { get; set; }   // 图片
        [DataType(DataType.Date)]
        public DateTime PublishDateTime { get; set; }  // 发布时间


        public int MessageNumber { get; set; } //留言次数

        public int Likes { get; set; } //点赞次数

        public virtual Person Person { get; set; } //归属的用户信息
        
        public Dynamic()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
