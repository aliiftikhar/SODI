using SODI.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Models {
   public class VoteEntity : IStackOverflowEntity {
      public string GetXmlFileName() {
         return "Votes.xml";
      }

      public string GetEntityName() {
         return "Votes";
      }

      public List<string> GetXmlAttributes() {

         List<string> attributes = new List<string>();
         attributes.Add("Id");
         attributes.Add("PostId");
         attributes.Add("VoteTypeId");
         attributes.Add("UserId");
         attributes.Add("CreationDate");
         attributes.Add("BountyAmount");

         return attributes;
      }
   }
}
