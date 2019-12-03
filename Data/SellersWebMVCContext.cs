using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SellersWebMVC.Models
{
    public class SellersWebMVCContext : DbContext
    {
        public SellersWebMVCContext (DbContextOptions<SellersWebMVCContext> options)
            : base(options)
        {
        }

        public DbSet<SellersWebMVC.Models.Department> Department { get; set; }
    }
}
