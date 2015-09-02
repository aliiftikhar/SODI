using SODI.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Models {
   public class BadgeEntity : IStackOverflowEntity {
      public string GetXmlFileName() {
         return "Badges.xml";
      }

      public string GetEntityName() {
         return "Badges";
      }

      public List<string> GetXmlAttributes() {
      
         List<string> attributes = new List<string>();
         attributes.Add("Id");
         attributes.Add("UserId");
         attributes.Add("Name");
         attributes.Add("Date");

         return attributes;
      }
   }
}
