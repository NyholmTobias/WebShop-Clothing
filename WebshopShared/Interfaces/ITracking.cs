using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebshopShared.Interfaces
{
    public interface ITracking
    {
        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

    }
}
