using SODI.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Models {
   public class CommentEntity : IStackOverflowEntity {
      public string GetXmlFileName() {
         return "Comments.xml";
      }

      public string GetEntityName() {
         return "Comments";
      }

      public List<string> GetXmlAttributes() {

         List<string> attributes = new List<string>();
         attributes.Add("Id");
         attributes.Add("PostId");
         attributes.Add("Score");
         attributes.Add("Text");
         attributes.Add("CreationDate");
         attributes.Add("UserId");
         attributes.Add("UserDisplayName");

         return attributes;
      }
   }
}
