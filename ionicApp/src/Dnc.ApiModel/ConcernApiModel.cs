using Dnc.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ApiModel
{
    public class ConcernApiModel
    {
        public virtual Guid PersonID { get; set; }     // 归属的用户ID
        public virtual string PersonNickname { get; set; } //归属的用户昵称
        public virtual string UserImage { get; set; } //归属的用户头像
        public ConcernApiModel(Concern bo)
        {
            if (bo.toid != null)
            {
                this.PersonID = bo.toid.ID;
                this.PersonNickname = bo.toid.Nickname;
                if (bo.toid.UserImage != null)
                    this.UserImage = Comm.ImgIP() + bo.toid.UserImage.Substring(6, bo.toid.UserImage.Length - 6);
                else
                    this.UserImage = null;
            }
        }
    }
}
