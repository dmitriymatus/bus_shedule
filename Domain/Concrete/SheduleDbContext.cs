using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Models;

namespace Domain.Concrete
{
   public class SheduleDbContext:DbContext
   {
      public DbSet<busStop> Stops { get; set; }
   }
}
