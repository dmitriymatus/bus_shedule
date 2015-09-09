using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication.Models;
using MvcApplication.Abstract;

namespace MvcApplication.Controllers
{
    public class AccountController : Controller
    {
       IAuthProvider provider;
       public AccountController(IAuthProvider _provider)
       {
          provider = _provider;
       }

       [HttpGet]
       public ActionResult Login(string returnUrl)
        {
           Session.Add("returnUrl", returnUrl);
           return View();
        }

       [HttpPost]
       public ActionResult Login(LoginViewModel model)
       {
          if (!ModelState.IsValid)
          {
             return View();
          }

          if (provider.Authenticate(model.UserName, model.Password))
          {
             return Redirect((string)Session["returnUrl"] ?? Url.Action("Index", "Admin"));
          }
          else
          {
             ModelState.AddModelError("", "Неправильно введено Имя пользователя и/или пароль!");
             return View();
          }

       }


    }
}
