using Dnc.Entities.Organization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ApiModel
{
    public class DynamicApiModel
    {
        public Guid ID { get; set; }
        public string Content { get; set; }   // 正文内容
        public string Image { get; set; }   // 图片
        public string PublishDateTime { get; set; }  // 发布时间
        public int MessageNumber { get; set; } //留言次数
        public int Likes { get; set; } //点赞次数
        public virtual Guid PersonID { get; set; }      // 归属的用户ID
        public virtual string PersonNickname { get; set; } //归属的用户昵称
        public virtual string userImage { get; set; } //归属的用户头像
        public DynamicApiModel(Dynamic bo)
        {
            this.ID = bo.ID;
            this.Content = bo.Content;
            this.PublishDateTime =Comm.DateStringFromNow(bo.PublishDateTime);
            if (bo.Image != null)
                this.Image = Comm.ImgIP() + bo.Image.Substring(6, bo.Image.Length - 6);
            else
                this.Image = null;
            this.MessageNumber = bo.MessageNumber;
            this.Likes = bo.Likes;
            if (bo.Person != null)
            {
                this.PersonID = bo.Person.ID;
                this.PersonNickname = bo.Person.Nickname;
                this.userImage = Comm.ImgIP() + bo.Person.UserImage.Substring(6, bo.Person.UserImage.Length - 6);
            }
        }
    }
}
