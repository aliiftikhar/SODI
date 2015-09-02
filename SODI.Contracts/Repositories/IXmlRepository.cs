using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SODI.Contracts.Repositories {
   public interface IXmlRepository {
      bool FileExist(string xmlFileName);
      XmlReader GetXmlReader(string xmlFileName);
   }
}
