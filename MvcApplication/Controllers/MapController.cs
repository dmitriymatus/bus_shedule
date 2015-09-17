using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Models;
using Domain.Abstract;
using MvcApplication.Models;
using MvcApplication.Concrete;

namespace MvcApplication.Controllers
{
    public class MapController : Controller
    {
       IStopsRepository repository;
       List<Station> stations = new List<Station>();
       public MapController(IStopsRepository _repository)
       {
          repository = _repository;

          // создадим список данных
          stations.Add(new Station()
          {
             Id = 1,
             Name = "АП",
             GeoLat = 23.745706,
             GeoLong = 52.102093,
          });
          stations.Add(new Station()
          {
             Id = 2,
             Name = "Чулочный комбинат",
             GeoLat = 23.734417,
             GeoLong = 52.103116,
          });
          stations.Add(new Station()
          {
             Id = 3,
             Name = "Чулочный комбинат",
             GeoLat = 23.735619,
             GeoLong = 52.103343,
          });
          stations.Add(new Station()
          {
             Id = 4,
             Name = "Ковры Бреста",
             GeoLat = 23.728479,
             GeoLong = 52.100252,
          });
          stations.Add(new Station()
          {
             Id = 5,
             Name = "Ковры Бреста",
             GeoLat = 23.729604,
             GeoLong = 52.100388,
          });

          stations.Add(new Station()
          {
             Id = 6,
             Name = "Киевская",
             GeoLat = 23.721275,
             GeoLong = 52.102358,
          });
          stations.Add(new Station()
          {
             Id = 7,
             Name = "Киевская",
             GeoLat = 23.722496,
             GeoLong = 52.101452,
          });

          stations.Add(new Station()
          {
             Id = 8,
             Name = "Березовка",
             GeoLat = 23.716032,
             GeoLong = 52.114793,
          });
          stations.Add(new Station()
          {
             Id = 9,
             Name = "Березовка",
             GeoLat = 23.715971,
             GeoLong = 52.114178,
          });

          stations.Add(new Station()
          {
             Id = 10,
             Name = "Радужная",
             GeoLat = 23.712152,
             GeoLong = 52.118220,
          });

          stations.Add(new Station()
          {
             Id = 11,
             Name = "Радужная",
             GeoLat = 23.713214,
             GeoLong = 52.117169,
          });
       }

        public ActionResult Index()
        {
           //var model = repository.GetAllStops();
           var model = stations.OrderBy(x => x.Name).Select(x => x.Name).Distinct();
            return View(model);
        }


        public JsonResult GetData()
        {
           

           return Json(stations, JsonRequestBehavior.AllowGet);
        }

    }
}
