using SODI.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Models {
   public class TagEntity : IStackOverflowEntity {
      public virtual string GetXmlFileName() {
         return "Tags.xml";
      }

      public virtual List<string> GetXmlAttributes() {

         List<string> attributes = new List<string>();

         attributes.Add("Id");
         attributes.Add("TagName");
         attributes.Add("Count");
         attributes.Add("ExcerptPostId");
         attributes.Add("WikiPostId");

         return attributes;
      }
      public virtual string GetEntityName() {
         return "Tags";
      }
   }
}
