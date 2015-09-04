using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Models
{
   public class HomeIndexViewModel
   {
      public IEnumerable<string> busNumber { get; set; }
      public IEnumerable<string> stopName { get; set; }
      public IEnumerable<string> endStop { get; set; }
      public IEnumerable<string> days { get; set; }
   }
}