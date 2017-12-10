using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Organization
{
    public class Person : IEntity
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(255)]
        public string UserImage { get; set; }      //头像

        [StringLength(50)]
        public string Password { get; set; }    //密码

        [StringLength(20)]
        public string Uumbers { get; set; }     //手机号

        [StringLength(20)]
        public string Nickname { get; set; }     //昵称

        [StringLength(500)]
        public string Description { get; set; }   //签名
        public int Concern { get; set; }  //关注
        public int Likes { get; set; }  //点赞
        public Person()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
