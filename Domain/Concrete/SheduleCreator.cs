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
      public static IEnumerable<busStop> Create(string fileName)
      {
         List<StringBuilder> rows = new List<StringBuilder>();
         FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);

         IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
         DataSet result = excelReader.AsDataSet();
         DataTable table = result.Tables[0];
         int k = 0;
         for(int i = 4; i< table.Rows.Count; i++)
         {
            if (table.Rows[i][0].ToString().StartsWith("№") == true)
            {
            StringBuilder item = new StringBuilder();           
            for (int j = 0; j < table.Columns.Count; j++ )
            {
               if ((table.Rows[i][j].ToString() as string) != null && table.Rows[i][0].ToString().StartsWith("№"))
               {
                  item.Append(table.Rows[i][j].ToString() + "|");
               }
            }
            rows.Add(item);
            }
         }
         IEnumerable<busStop> stops = Parse(rows);
         stream.Close();
         excelReader.Dispose();
         return stops;
      }


      private static IEnumerable<busStop> Parse(List<StringBuilder> rows)
      {
         List<busStop> result = new List<busStop>();
         string busNumber;
         string stopName;
         string finalStop;
         string days;
         string stops;
         string[] cols;
         char[] separator = new char[1];
         separator[0] = '|';

         foreach(StringBuilder row in rows)
         {
            try
            {
               cols = row.ToString().Split(separator, StringSplitOptions.None);
               busNumber = cols[0].Remove(0, 1);
               stopName = cols[3];
               finalStop = cols[2];
               days = cols[1] == "Р" ? "Рабочие" : cols[1] == "В" ? "Выходные" : cols[1] == "Р,В" ? "Ежедневно" : cols[1];
               stops = Convert(cols.Skip(7).Take(cols.Count() - 11));
               result.Add(new busStop { busNumber = busNumber, stopName = stopName, finalStop = finalStop, days = days, stops = stops });
            }
            catch
            {

            }
         }

         return result;
      }


      private static string Convert(IEnumerable<string> values)
      {
         StringBuilder result = new StringBuilder();
         double temp;
         int hours;
         int minutes;
         string answer;

         foreach(string value in values)
         {
            if(value != "")
            {
            temp = double.Parse(value);
            hours = (int)(temp * 24);
            minutes = (int)Math.Round((((temp * 24) - (double)hours)*60));
               if(minutes >= 60)
               {
                  hours += 1;
                  minutes -= 60;
               }
            answer = string.Format("{0:00}", hours) + string.Format(":{0:00}", minutes);
            result.Append(answer + " ");
            }
         }
         return result.ToString();
      }


   }
}
