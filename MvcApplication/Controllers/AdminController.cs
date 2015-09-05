using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using MvcApplication.Models;
using System.IO;

namespace MvcApplication.Controllers
{
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
                 ViewBag.Erors = "Неправильное расширение файла, загрузите файл с расширением xls";
                 return View();
              }
              else
              {
                 try
                 {
                    var fileName = this.HttpContext.Request.MapPath("~/Content/shedule.xls");
                   

                    file.SaveAs(fileName);
                    var answer = SheduleCreator.Create(fileName);
                    ViewBag.Success = "Расписание добавлено";
                 }
                 catch
                 {
                    ViewBag.Erors = "Ошибка при обработке файла, проверьте правильность файла";
                 }                 
                 return View();
              }
           }
           else
           {
              ViewBag.Erors = "Выберите файл";
              return View();
           }
        }
    }
}
