using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Models {
   public class DatabaseDetails {
      public DatabaseType Type { get; set; }
      public string Server { get; set; }
      public string Name { get; set; }
      public string Username { get; set; }
      public string Password { get; set; }
   }
}
