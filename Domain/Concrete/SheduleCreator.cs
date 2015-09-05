using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Reflection;
using Excel;
using Domain.Models;
using System.Text.RegularExpressions;


namespace Domain.Concrete
{
   public static class SheduleCreator
   {
      public static string Create(string fileName)
      {
         List<StringBuilder> rows = new List<StringBuilder>();
         FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);

         IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
         DataSet result = excelReader.AsDataSet();
         DataTable table = result.Tables[0];

         for(int i = 5; i< table.Rows.Count; i++)
         {
            rows.Add(new StringBuilder());
            for (int j = 0; j < table.Columns.Count; j++ )
            {
               if ((table.Rows[i][j].ToString() as string) != null && table.Rows[i][0].ToString().StartsWith("№"))
               {
                  rows[i-5].Append(table.Rows[i][j].ToString() + " ");
               }
            }
         }
         IEnumerable<busStop> stops = Parse(rows);
         return "hello";
      }


      public static IEnumerable<busStop> Parse(List<StringBuilder> rows)
      {
         char[] separator = new char[1];
         separator[0] = ' ';
         string[] cols = rows[0].ToString().Split(separator,StringSplitOptions.RemoveEmptyEntries);
         Regex regex = new Regex("");
         return new List<busStop>();
      }

   }
}
