using Dnc.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ApiModel
{
    public class DynamicDetailsApiModel
    {
        public Guid ID { get; set; }
        public string Content { get; set; }   // 正文内容
        public string Image { get; set; }   // 图片
        public string PublishDateTime { get; set; }  // 发布时间
        public int MessageNumber { get; set; } //留言次数
        public int Likes { get; set; } //点赞次数

        public DynamicDetailsApiModel(Dynamic bo)
        {
            this.ID = bo.ID;
            this.Content = bo.Content;
            this.PublishDateTime = Comm.DateStringFromNow(bo.PublishDateTime);
            if (bo.Image != null)
                this.Image = Comm.ImgIP() + bo.Image.Substring(6, bo.Image.Length - 6);
            else
                this.Image = null; 
            this.MessageNumber = bo.MessageNumber;
            this.Likes = bo.Likes;
        }
    }
}
