using SODI.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Models {
   public class UserEntity : IStackOverflowEntity {
      public string GetXmlFileName() {
         return "Users.xml";
      }

      public string GetEntityName() {
         return "Users";
      }

      public List<string> GetXmlAttributes() {

         List<string> attributes = new List<string>();
         attributes.Add("Id");
         attributes.Add("Reputation");
         attributes.Add("CreationDate");
         attributes.Add("DisplayName");
         attributes.Add("LastAccessDate");
         attributes.Add("WebsiteUrl");
         attributes.Add("Location");
         attributes.Add("AboutMe");
         attributes.Add("Views");
         attributes.Add("UpVotes");
         attributes.Add("DownVotes");
         attributes.Add("AccountId");
         attributes.Add("Age");
         attributes.Add("ProfileImageUrl");

         return attributes;
      }
   }
}
