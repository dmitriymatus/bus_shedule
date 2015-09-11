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
      IEnumerable<string> GetBuses();
      IEnumerable<string> GetStops(string busNumber);
      IEnumerable<string> GetOtherBuses(string stopName, string busNumber);
      IEnumerable<string> GetFinalStops(string stopName, string busNumber);
      IEnumerable<string> GetDays(string stopName, string busNumber, string endStop);
      IEnumerable<string> GetItems(string stopName, string busNumber, string endStop, string days);
      void AddStops(IEnumerable<busStop> stops);
      void AddStop(busStop stop);
      bool Contain(busStop stop);
      void DeleteAll();
      bool Update(busStop stop);
      bool Delete(busStop stop);
   }
}
