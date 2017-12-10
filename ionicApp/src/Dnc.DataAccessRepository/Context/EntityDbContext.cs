using Dnc.Entities.Application;
using Dnc.Entities.Organization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.DataAccessRepository.Context
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options)
           : base(options) { }
        public DbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Person> Persons { get; set; }

        public DbSet<Dynamic> Dynamics { get; set; }
        public DbSet<Concern> Concerns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                //optionsBuilder.UseSqlServer(
                //    @"Server=222.217.36.123\\lzzyStudent,10175;Initial Catalog=CPMD_Guide2016; uid=lion;pwd=zaq!@wsx199402;MultipleActiveResultSets=True");
                //base.OnConfiguring(optionsBuilder);

                //远程链接数据库
                optionsBuilder.UseSqlServer(
                  @"Server=192.168.100.19;Initial Catalog=ionicDB; uid=sa;pwd=Admin123;MultipleActiveResultSets=True");
                base.OnConfiguring(optionsBuilder);

                //本地
                //optionsBuilder.UseSqlServer(
                // @"Server=localhost;Initial Catalog=ionicDB; uid=sa;pwd=123;MultipleActiveResultSets=True");
                //base.OnConfiguring(optionsBuilder);
            }
        }
    }
}
