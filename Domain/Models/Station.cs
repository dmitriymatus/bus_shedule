using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class Station
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public double GeoLong { get; set; } // долгота - для карт google
      public double GeoLat { get; set; } // широта - для карт google
   }
}
