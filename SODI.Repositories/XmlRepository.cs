using SODI.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SODI.Repositories {
   public class XmlRepository : IXmlRepository {

      private string xmlSourceFolderPathIncludingLastSlash;

      public XmlRepository(string xmlSourceFolderPathIncludingLastSlash) {
         this.xmlSourceFolderPathIncludingLastSlash = xmlSourceFolderPathIncludingLastSlash;
      }

      public bool FileExist(string xmlFileName) {
         return File.Exists(xmlSourceFolderPathIncludingLastSlash + xmlFileName);
      }

      public XmlReader GetXmlReader(string xmlFileName) {
         return XmlReader.Create(xmlSourceFolderPathIncludingLastSlash + xmlFileName);
      }
   }
}
