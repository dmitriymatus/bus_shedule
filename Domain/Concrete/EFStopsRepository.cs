﻿using System;
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
//-------------------------------------------------------------------------------
#region getMethods

      public IEnumerable<string> GetBuses()
      {
         //получение номеров автобусов
         return context.Stops.AsEnumerable()
                             .OrderBy(x => x.Id)
                             .Select(x => x.busNumber)
                             .Distinct();   
      }

      public IEnumerable<string> GetAllStops()
      {
         //получение названий всех остановок
         return context.Stops.AsEnumerable()
                    .OrderBy(x => x.stopName)
                    .Select(x => x.stopName)
                    .Distinct(); 
      }

      public IEnumerable<string> GetStops(string busNumber)
      {
         //получение названий остановок
         return context.Stops.AsEnumerable()
                             .Where(x => x.busNumber == busNumber)
                             .Select(x => x.stopName)
                             .Distinct();          
      }

      public IEnumerable<string> GetOtherBuses(string stopName, string busNumber)
      {
         //получение других автобусов на этой остановке
         return context.Stops.Where(x => x.stopName == stopName && x.busNumber != busNumber)
                             .Select(x => x.busNumber)
                             .Distinct();
      }


      public IEnumerable<string> GetFinalStops(string stopName, string busNumber)
      {
         //получение конечных остановок
         return context.Stops.Where(x => x.stopName == stopName && x.busNumber == busNumber)
                             .Select(x=>x.finalStop)
                             .Distinct();         
      }

      public IEnumerable<string> GetDays(string stopName, string busNumber, string endStop)
      {
         //получение дней
         return context.Stops.Where(x => x.stopName == stopName && x.busNumber == busNumber && x.finalStop == endStop)
                             .Select(x=>x.days)
                             .Distinct();
      }

      public IEnumerable<string> GetItems(string stopName, string busNumber, string endStop, string days)
      {
         //получение времени остановок
         String stops = context.Stops.First(x => x.busNumber == busNumber && x.stopName == stopName && x.finalStop == endStop && x.days == days).stops;
         Regex reg = new Regex(@"\d{1,2}:\d{1,2}");
         MatchCollection matches = reg.Matches(stops);
         
         return matches.Cast<Match>().Select(x => x.Value);
      }
#endregion 
//----------------------------------------------------------------------------------------------------------

//--------------------------------------------------------------------------------------------------
#region adminMethods

      public void AddStop(busStop stop)
      {
         if (stop != null)
            {
               context.Stops.Add(stop);
               context.SaveChanges();
            }         
      }

      public void AddStops(IEnumerable<busStop> stops)
      {
         context.Stops.AddRange(stops);
         context.SaveChanges();
      }

      public void DeleteAll()
      {
         IEnumerable<busStop> stops = context.Stops;
         context.Stops.RemoveRange(stops);
         context.SaveChanges();
      }

      public bool Contain(busStop stop)
      {
         bool result;
         IEnumerable<busStop> list = Filter(stop);
         if (list.Count() != 0)
         {
            result = true;
         }
         else
         {
            result = false;
         }
         return result;
      }

      public bool Update(busStop stop)
      {
         bool result;
         busStop item = Filter(stop).FirstOrDefault();
         if (item != null)
         {
            item.stops = stop.stops;
            context.SaveChanges();
            result = true;
         }
         else
         {
            result = false;
         }
         return result;
      }

      public bool Delete(busStop stop)
      {
         bool result;
         busStop item = Filter(stop).FirstOrDefault();
         if (item != null)
         {
            context.Stops.Remove(item);
            context.SaveChanges();
            result = true;
         }
         else
         {
            result = false;
         }
         return result;
      }
#endregion
//-----------------------------------------------------------------------------------

      private IEnumerable<busStop> Filter(busStop stop)
      {
         return context.Stops.Where(x => x.busNumber == stop.busNumber && x.stopName == stop.stopName && x.finalStop == stop.finalStop && x.days == stop.days);
      }
   }
}
