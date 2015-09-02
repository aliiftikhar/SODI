using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Contracts.Models {
   public interface IStackOverflowEntity {
      string GetXmlFileName();
      string GetEntityName();
      List<string> GetXmlAttributes();
   }
}
