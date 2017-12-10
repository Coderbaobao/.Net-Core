using Dnc.DataAccessRepository.Context;
using Dnc.Entities.Application;
using Dnc.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.DataAccessRepository.Seeds
{
    public static class DbInitializer
    {
        public static void Initialze(EntityDbContext context)
        {

            context.Database.EnsureCreated();
            if (context.ApplicationUsers.Any())
                return;

            var appGroups = new List<ApplicationGroup> {
                new ApplicationGroup {Name="系统管理员组", Decription="具备使用系统全部权限的用户组。" },
                new ApplicationGroup {Name="授权用户组", Decription="具备指定权限的用户组" },
                new ApplicationGroup {Name="普通访客组", Decription="仅仅注册资料的，具有常规的公开业务模块使用权限的用户组" }
            };
            foreach (var item in appGroups)
            {
                context.ApplicationGroups.Add(item);
            }
            context.SaveChanges();

            var appUsers = new List<ApplicationUser>
            {
                new ApplicationUser {Name="tiger",Password="123", Group=appGroups[0], ApplicationGroupID=appGroups[0].ID },
                new ApplicationUser {Name="lion",Password="123", Group=appGroups[1], ApplicationGroupID=appGroups[0].ID },
                new ApplicationUser {Name="rabbit",Password="123", Group=appGroups[2], ApplicationGroupID=appGroups[0].ID }
            };
            foreach (var item in appUsers)
            {
                context.ApplicationUsers.Add(item);
            }
            context.SaveChanges();
            var personInfo = new List<Person>
            {
                new Person {Nickname="总有贱人想害朕",Uumbers="18407720000", Password="123",Concern=0,Likes=0,Description="要是能重来，我要选李白",  UserImage="../../img/adam.jpg" },
                new Person {Nickname="总有逗比挑衅本尊",Uumbers="18407720001", Password="123",Concern=0,Likes=0,Description="要是能重来，我要选李白",UserImage="../../img/ben.png" },
                new Person {Nickname="坑爹又拼爹的年代",Uumbers="18407720002", Password="123",Concern=0,Likes=0,Description="要是能重来，我要选李白", UserImage="../../img/newsDemo.png" },
                new Person {Nickname="凉辰梦瑾空人心",Uumbers="18407720003", Password="123",Concern=0,Likes=0,Description="要是能重来，我要选李白", UserImage="../../img/me.png" },
                new Person {Nickname="就算全世界与我为敌",Uumbers="18407720004", Password="123",Concern=0,Likes=0,Description="要是能重来，我要选李白",UserImage="../../img/perry.png" },
                new Person {Nickname="人丑嘴不甜长的磕碜又没钱",Uumbers="18407720005", Password="123",Concern=13,Likes=0,Description="要是能重来，我要选李白",UserImage="../../img/mike.png"},
                new Person {Nickname="你看我6不6",Uumbers="18407720006", Password="123",Concern=0,Likes=0,Description="要是能重来，我要选李白",UserImage="../../img/max.png"},
            };
            foreach (var item in personInfo)
                context.Persons.Add(item);
            context.SaveChanges();  
        }
    }
}
