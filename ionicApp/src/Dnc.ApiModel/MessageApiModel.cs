using Dnc.Entities.Organization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ApiModel
{
    public class MessageApiModel
    {
        public Guid ID { get; set; }
        public string Content { get; set; }            //留言内容
        public string Messagestime { get; set; }       //留言时间
        public int Likes { get; set; }            //点赞次数
        public virtual Guid PersonID { get; set; }      // 归属的用户ID
        public virtual string PersonNickname { get; set; }   //归属的用户名称
        public virtual string userImage { get; set; }   //归属的用户头像
        public MessageApiModel() { }
        public MessageApiModel(Message bo)
        {
            this.ID = bo.ID;
            this.Content = bo.Content;
            this.Messagestime = Comm.DateStringFromNow(bo.Messagestime);
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
