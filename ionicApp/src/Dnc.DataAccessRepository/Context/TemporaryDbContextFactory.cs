using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.DataAccessRepository.Context
{
    public class TemporaryDbContextFactory : IDbContextFactory<EntityDbContext>
    {
        public EntityDbContext Create(DbContextFactoryOptions options)
        {
            //var builder = new DbContextOptionsBuilder<EntityDbContext>();
            //builder.UseSqlServer("Server=222.217.36.123\\lzzyStudent,10175;Initial Catalog=CPMD_Guide2016; uid=lion;pwd=zaq!@wsx199402;MultipleActiveResultSets=True");
            //return new EntityDbContext(builder.Options);

            var builder = new DbContextOptionsBuilder<EntityDbContext>();
            builder.UseSqlServer("Server=192.168.100.19;Initial Catalog=IonicDB; uid=sa;pwd=Admin123;MultipleActiveResultSets=True");
            return new EntityDbContext(builder.Options);

            //var builder = new DbContextOptionsBuilder<EntityDbContext>();
            //builder.UseSqlServer("Server=localhost;Initial Catalog=IonicDB; uid=sa;pwd=123;MultipleActiveResultSets=True");
            //return new EntityDbContext(builder.Options);
        }
    }
}
