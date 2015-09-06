using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Models;
using System.Text.RegularExpressions;

namespace Domain.Concrete
{
   public class EFStopsRepository:IStopsRepository
   {
      SheduleDbContext context = new SheduleDbContext();
      public IEnumerable<busStop> Stops
      {
         get { return context.Stops; }
      }

      public IEnumerable<string> getStops(string busNumber)
      {
         IEnumerable<string> result = context.Stops.Where(x => x.busNumber == busNumber).GroupBy(x => x.stopName).Select(group => group.FirstOrDefault().stopName).ToList();
         return result;
      }

      public IEnumerable<string> getFinalStops(string stopName, string busNumber)
      {
         IEnumerable<string> result = context.Stops.Where(x => x.stopName == stopName && x.busNumber == busNumber).GroupBy(x => x.finalStop).Select(group => group.FirstOrDefault().finalStop).ToList();
         return result;
      }

      public IEnumerable<string> getDays(string stopName, string busNumber, string endStop)
      {
         IEnumerable<string> result = context.Stops.Where(x => x.stopName == stopName && x.busNumber == busNumber && x.finalStop==endStop).GroupBy(x => x.days).Select(group => group.FirstOrDefault().days).ToList();
         return result;
      }

      public IEnumerable<string> getItems(string stopName, string busNumber, string endStop, string days)
      {
         List<string> result = new List<string>();
         //получение времени остановок
         String stops = context.Stops.Where(x => x.busNumber == busNumber && x.stopName == stopName && x.finalStop == endStop && x.days == days).First().stops;
         Regex reg = new Regex(@"\d{1,2}:\d{1,2}");
         MatchCollection matches = reg.Matches(stops);
         foreach(Match match in matches)
         {
            result.Add(match.Value);
         }
         return result;
      }

      public void AddStops(IEnumerable<busStop> stops)
      {
         foreach(busStop item in stops)
         {
            if (item != null)
            {
               context.Stops.Add(item);
            }
         }
         context.SaveChanges();
      }

      public void DeleteAll()
      {
         IEnumerable<busStop> stops = context.Stops;
         context.Stops.RemoveRange(stops);
         context.SaveChanges();
      }


   }
}
