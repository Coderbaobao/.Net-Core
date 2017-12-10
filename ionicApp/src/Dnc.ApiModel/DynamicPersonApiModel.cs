using Dnc.Entities.Organization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ApiModel
{
    public class DynamicPersonApiModel
    {
        public Guid ID { get; set; }
        public string UserImage { get; set; }     //头像
        public string Nickname { get; set; }      //昵称
        public string Description { get; set; }    //签名
        public int Concern { get; set; }  //关注
        public int Likes { get; set; }  //点赞
        public DynamicPersonApiModel() { }
        public DynamicPersonApiModel(Person bo)
        {
            this.ID = bo.ID;
            if (bo.UserImage != null)
                this.UserImage = Comm.ImgIP() + bo.UserImage.Substring(6, bo.UserImage.Length - 6);
            else this.UserImage = null;
            this.Nickname = bo.Nickname;
            this.Description = bo.Description;
            this.Concern = bo.Concern;
            this.Likes = bo.Likes;
        }
    }
}
