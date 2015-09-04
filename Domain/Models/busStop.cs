using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class busStop
   {
      public int Id { get; set; }
      public string busNumber { get; set; }
      public string stopName { get; set; }
      public string stops { get; set; }
      public string finalStop { get; set; }
      public string days { get; set; }
   }
}
