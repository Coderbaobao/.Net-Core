using Dnc.DataAccessRepository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.DataAccessRepository.Utilities
{
    public static class DataAccessUtility
    {
        private static EntityDbContext _Context;

        static DataAccessUtility()
        {
        }

        public static void InitialDBContext(EntityDbContext context)
        {
            _Context = context;
        }

        public static string GetGroupNameByUserID(Guid id)
        {
            var result = "";
            var user = _Context.ApplicationUsers.FirstOrDefault(x => x.ID == id);
            if (user != null)
            {
                var group = _Context.ApplicationGroups.FirstOrDefault(x => x.ID == user.ApplicationGroupID);
                if (group != null)
                {
                    result = group.Name;
                }
            }
            return result;
        }
    }
}
