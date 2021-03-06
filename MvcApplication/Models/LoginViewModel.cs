﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication.Models
{
   public class LoginViewModel
   {
      [Required]
      [Display(Name = "Имя пользователя")]
      public string UserName { get; set; }

      [Required]
      [Display(Name = "Пароль")]
      public string Password { get; set; }
   }
}