using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using MvcApplication.Models;
using System.Threading;

namespace MvcApplication.Controllers
{
    public class HomeController : Controller
    {
       IStopsRepository repository;
       public HomeController(IStopsRepository _repository)
       {
          repository = _repository;
       }

       public ActionResult Index()
       {
          //получение номеров автобусов
          IEnumerable<string> buses = repository.Stops.OrderBy(x => x.Id).GroupBy(x => x.busNumber).Select(group => group.First().busNumber).ToList();
          HomeIndexViewModel model = new HomeIndexViewModel { busNumber = buses, stopName = new List<string>(), endStop = new List<string>(), days = new List<string>() };
          return View(model);
       }

       public JsonResult getStopsNames(string busNumber)
       {
          IEnumerable<string> stops;
          //получение названий остановок
          stops = repository.getStops(busNumber);
          var result = new { Stops = stops};          
          return Json(result, JsonRequestBehavior.AllowGet);
       }

       public JsonResult GetFinalStops(string stopName, string busNumber)
       {
          //получение конечных остановок
          IEnumerable<string> result = repository.getFinalStops(stopName, busNumber);
          return Json(result, JsonRequestBehavior.AllowGet);
       }


       public JsonResult GetDays(string stopName, string busNumber, string endStop)
       {
          //получение дней
          IEnumerable<string> result = repository.getDays(stopName, busNumber, endStop);
          return Json(result, JsonRequestBehavior.AllowGet);
       }

       public JsonResult getStops(string busNumber, string stopName, string endStopName, string days)
       {

          //получение дней
          IEnumerable<string> result = repository.getItems(stopName, busNumber, endStopName, days);
          return Json(result, JsonRequestBehavior.AllowGet);
       }
    }
}
