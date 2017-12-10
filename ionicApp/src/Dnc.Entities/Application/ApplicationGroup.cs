using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Dnc.Entities.Application
{
    public class ApplicationGroup : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Decription { get; set; }

        public ApplicationGroup()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
