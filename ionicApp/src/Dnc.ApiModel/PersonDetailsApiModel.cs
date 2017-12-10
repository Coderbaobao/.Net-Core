using Dnc.Entities.Organization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ApiModel
{
    public class PersonDetailsApiModel
    {

        public Guid ID { get; set; }
        public string UserImage { get; set; }     //头像
        public string Password { get; set; }      //密码
        public string Uumbers { get; set; }      //手机号
        public string Nickname { get; set; }      //昵称
        public string Description { get; set; }    //签名

        public PersonDetailsApiModel() { }
        public PersonDetailsApiModel(Person bo)
        {
            this.ID = bo.ID;
            this.UserImage = Comm.ImgIP() + bo.UserImage.Substring(6, bo.UserImage.Length - 6);
            this.Password = bo.Password;
            this.Uumbers = bo.Uumbers;
            this.Nickname = bo.Nickname;
            this.Description = bo.Description;

        }
    }
}
