using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Organization
{
    public class Message : IEntity
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(500)]
        public string Content { get; set; }            //留言内容

        [DataType(DataType.Date)]
        public DateTime Messagestime { get; set; }       //留言时间
        public int Likes { get; set; }        //点赞次数

        [StringLength(255)]

        public virtual Person Person { get; set; }  //归属的用户信息
        public virtual Dynamic Dynamic { get; set; }  //归属的动态信息
        public Message()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
