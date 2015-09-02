using SODI.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Models {
   public class PostEntity : IStackOverflowEntity {
      public string GetXmlFileName() {
         return "Posts.xml";
      }

      public string GetEntityName() {
         return "Posts";
      }

      public List<string> GetXmlAttributes() {

         List<string> attributes = new List<string>();
         attributes.Add("Id");
         attributes.Add("PostTypeId");
         attributes.Add("AcceptedAnswerId");
         attributes.Add("CreationDate");
         attributes.Add("Score");
         attributes.Add("ViewCount");
         attributes.Add("Body");
         attributes.Add("OwnerUserId");
         attributes.Add("LastEditDate");
         attributes.Add("LastActivityDate");
         attributes.Add("Title");
         attributes.Add("Tags");
         attributes.Add("AnswerCount");
         attributes.Add("CommentCount");
         attributes.Add("FavoriteCount");
         attributes.Add("ClosedDate");
         attributes.Add("CommunityOwnedDate");
         attributes.Add("ParentId");
         attributes.Add("OwnerDisplayName");
         attributes.Add("LastEditorDisplayName");

         return attributes;
      }
   }
}
