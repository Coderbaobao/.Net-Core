using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Organization
{
    public class Concern : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        public virtual Person myid { get; set; }  
        public virtual Person toid { get; set; }
        public Concern()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
