using SODI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Console {
   public class Logger : ILogger {
      public void Log(string message) {
         System.Console.WriteLine(message);
      }
   }
}
