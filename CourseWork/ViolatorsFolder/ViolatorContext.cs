using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal
{
    public class ViolatorContext : DbContext
    {
        public ViolatorContext() : base("DefaultConnection") { }
        public DbSet<Violator> Violators { get; set; }
    }
}
