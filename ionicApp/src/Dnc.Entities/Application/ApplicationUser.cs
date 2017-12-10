﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    public class ApplicationUser : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Password { get; set; }

        public Guid ApplicationGroupID { get; set; }
        public virtual ApplicationGroup Group { get; set; }

        public ApplicationUser()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
