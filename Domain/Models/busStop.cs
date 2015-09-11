using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
   public class busStop : IEquatable<busStop>
   {
      public int Id { get; set; }
      [Required]
      [Display(Name="Номер")]
      public string busNumber { get; set; }
      [Required]
      [Display(Name = "Остановка")]
      public string stopName { get; set; }
      [Required]
      [Display(Name = "Расписание")]
      public string stops { get; set; }
      [Required]
      [Display(Name = "Конечная")]
      public string finalStop { get; set; }
      [Required]
      [Display(Name = "Дни")]
      public string days { get; set; }

      public bool Equals(busStop other)
      {
         if (other.busNumber == busNumber)
         {
            return true;
         }
         else
         {
            return false;
         }
      }

      public override int GetHashCode()
      {
         return this.busNumber.GetHashCode();
      }


   }
}
