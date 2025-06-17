using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Shared.Models
{
    public class Role
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        // Navigation property
        public ICollection<User>? Users { get; set; }
    }
}
