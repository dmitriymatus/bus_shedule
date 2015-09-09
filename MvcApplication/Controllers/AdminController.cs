using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Models;
using MvcApplication.Models;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace MvcApplication.Controllers
{
   [Authorize]
    public class AdminController : Controller
    {
       IStopsRepository repository;
       public AdminController(IStopsRepository _repository)
       {
          repository = _repository;
       }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
           return View();
        }

        [HttpPost]
        public ActionResult Add(HttpPostedFileBase file)
        {
           if (file != null)
           {
              if (file.ContentType != "application/vnd.ms-excel")
              {
                 TempData["Erors"] = "Неправильное расширение файла, загрузите файл с расширением xls";
                 return View();
              }
              else
              {
                 try
                 {
                    var fileName = this.HttpContext.Request.MapPath("~/Content/shedule.xls");
                    file.SaveAs(fileName);

                    IEnumerable<busStop> answer = SheduleCreator.Create(fileName);
                    repository.DeleteAll();
                    repository.AddStops(answer);
                    TempData["Success"] = "Расписание добавлено";
                 }
                 catch
                 {
                    TempData["Erors"] = "Ошибка при обработке файла, проверьте правильность файла";
                 }                 
                 return View();
              }
           }
           else
           {
              TempData["Erors"] = "Выберите файл";
              return View();
           }
        }

        [HttpGet]
        public ActionResult AddStop()
        {

           var numbers = repository.Stops.GroupBy(x => x.busNumber).Select(group => group.First().busNumber).ToList();
           var stopNames = repository.Stops.GroupBy(x => x.stopName).Select(group => group.First().stopName).OrderBy(x => x).ToList();
           var finalStops = repository.Stops.GroupBy(x => x.finalStop).Select(group => group.First().finalStop).OrderBy(x => x).ToList();
           var days = repository.Stops.GroupBy(x => x.days).Select(group => group.First().days).OrderBy(x => x).ToList();
           AdminAddViewModel model = new AdminAddViewModel { Numbers = numbers, StopNames = stopNames, Days = days, FinalStops = finalStops, Stop = new busStop()};
           return View(model);
        }

       [HttpPost]
       public ActionResult AddStop(busStop stop)
       {
          var numbers = repository.Stops.GroupBy(x => x.busNumber).Select(group => group.First().busNumber).ToList();
          var stopNames = repository.Stops.GroupBy(x => x.stopName).Select(group => group.First().stopName).OrderBy(x => x).ToList();
          var finalStops = repository.Stops.GroupBy(x => x.finalStop).Select(group => group.First().finalStop).OrderBy(x => x).ToList();
          var days = repository.Stops.GroupBy(x => x.days).Select(group => group.First().days).OrderBy(x => x).ToList();
          if (!ModelState.IsValid)
          {
             AdminAddViewModel model = new AdminAddViewModel { Numbers = numbers, StopNames = stopNames, Days = days, FinalStops = finalStops, Stop = stop };
             return View(model);
          }

          Regex reg = new Regex(@"\d{1,2}:\d{1,2}");
            MatchCollection matches = reg.Matches(stop.stops);
            if (matches.Count == 0)
            {
               ModelState.AddModelError("", "Неправильно заполнено расписание");
               AdminAddViewModel model = new AdminAddViewModel { Numbers = numbers, StopNames = stopNames, Days = days, FinalStops = finalStops, Stop = stop };
               return View(model);
            }
            else if (repository.Contain(stop))
            {
               ModelState.AddModelError("", "Запись уже существует");
               AdminAddViewModel model = new AdminAddViewModel { Numbers = numbers, StopNames = stopNames, Days = days, FinalStops = finalStops, Stop = stop };
               return View(model);
            }
            else
            {
               StringBuilder stops = new StringBuilder();
               foreach (Match match in matches)
               {
                  string time = match.Value;
                  stops.Append(time + " ");                 
               }
               stop.stops = stops.ToString();
               repository.AddStop(stop);
               TempData["Success"] = "Запись добавлена";
            }
          return RedirectToAction("AddStop");
       }


       [HttpGet]
       [OutputCache(Duration = 1, NoStore = true)]
       public ActionResult Edit()
       {          
          IEnumerable<string> buses = repository.Stops.GroupBy(x => x.busNumber).Select(group => group.First().busNumber).ToList();
          HomeIndexViewModel model = new HomeIndexViewModel { busNumber = buses, stopName = new List<string>(), endStop = new List<string>(), days = new List<string>() };
          return View(model);
       }


       [HttpPost]
       public ActionResult Edit(busStop stop)
       {
          if (repository.Update(stop))
          {
             TempData["Success"] = "Запись обновлена";
          }
          else
          {
             TempData["Erors"] = "Запись не обновлена";
          }
          return RedirectToAction("Edit");
       }

       [HttpGet]
       [OutputCache(Duration = 1, NoStore = true)]
       public ActionResult Delete()
       {
          IEnumerable<string> buses = repository.Stops.GroupBy(x => x.busNumber).Select(group => group.First().busNumber).ToList();
          HomeIndexViewModel model = new HomeIndexViewModel { busNumber = buses, stopName = new List<string>(), endStop = new List<string>(), days = new List<string>() };
          return View(model);
       }


       [HttpPost]
       public ActionResult Delete(busStop stop)
       {
          if (repository.Delete(stop))
          {
             TempData["Success"] = "Запись удалена";
          }
          return RedirectToAction("Delete");
       }

       [HttpGet]
       public ActionResult DeleteAll()
       {
          repository.DeleteAll();
          TempData["Success"] = "Записи удалены";
          return RedirectToAction("Index");
       }


    }
}
