using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplication.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication.Models.Admin
{
   public class AddFileViewModel
   {
      [Required(ErrorMessage = "Выберите файл")]
      [DataType(DataType.Upload)]
      [FileSize(20000000,ErrorMessage = "Максимальный размер файла не должен превышать 20MB")]
      public HttpPostedFileBase file { get; set; }
   }
}