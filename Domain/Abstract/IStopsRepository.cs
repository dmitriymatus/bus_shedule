using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Abstract
{
   public interface IStopsRepository
   {
      IEnumerable<busStop> Stops { get; }
      IEnumerable<string> getStops(string busNumber);
      IEnumerable<string> getFinalStops(string stopName, string busNumber);
      IEnumerable<string> getDays(string stopName, string busNumber, string endStop);
      IEnumerable<string> getItems(string stopName, string busNumber, string endStop, string days);
      void AddStops(IEnumerable<busStop> stops);
      void DeleteAll();
   }
}
