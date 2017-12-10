using Dnc.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ApiModel
{
    public class PersonSimpleApiModel
    {
        public Guid ID { get; set; }
        public string UserImage { get; set; }     //头像
        public string Nickname { get; set; }      //昵称
        public PersonSimpleApiModel(Person bo)
        {
            this.ID = bo.ID;          
            this.UserImage = Comm.ImgIP() + bo.UserImage.Substring(6, bo.UserImage.Length - 6);       
            this.Nickname = bo.Nickname;
        }
    }
}
