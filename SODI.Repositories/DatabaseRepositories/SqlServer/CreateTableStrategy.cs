using SODI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Repositories.DatabaseRepositories.SqlServer {
   public class CreateTableStrategy {

      Dictionary<Type, string> cretaeTableSql;
      public CreateTableStrategy() {
         cretaeTableSql = new Dictionary<Type, string>();

         cretaeTableSql.Add(typeof(TagEntity), CreateTagsTableSql());
         cretaeTableSql.Add(typeof(UserEntity), CreateUsersTableSql());
         cretaeTableSql.Add(typeof(BadgeEntity), CreateBadgesTableSql());
         cretaeTableSql.Add(typeof(VoteEntity), CreateVotesTableSql());
         cretaeTableSql.Add(typeof(CommentEntity), CreateCommentsTableSql());
         cretaeTableSql.Add(typeof(PostEntity), CreatePostsTableSql());
      }

      public string GetCreateTableSql(Type type) {
         return cretaeTableSql[type];
      }

      private string CreateTagsTableSql() {
         return @"CREATE TABLE [dbo].[Tags](
	                  [Id] [int] IDENTITY(1,1) PRIMARY KEY,
	                  [TagName] [nvarchar](max) NULL,
	                  [Count] [int] NULL,
	                  [ExcerptPostId] [int] NULL,
	                  [WikiPostId] [int] NULL)";
      }

      private string CreateUsersTableSql() {
         return @"CREATE TABLE [dbo].[Users](
	                  [Id] [int] IDENTITY(1,1) PRIMARY KEY,
	                  [Reputation] [nvarchar](max) NULL,
	                  [CreationDate] [datetime] NULL,
	                  [DisplayName] [nvarchar](max) NULL,
	                  [LastAccessDate] [datetime] NULL,
	                  [WebsiteUrl] [nvarchar](max) NULL,
	                  [Location] [nvarchar](max) NULL,
	                  [AboutMe] [nvarchar](max) NULL,
	                  [Views] [nvarchar](max) NULL,
	                  [UpVotes] [nvarchar](max) NULL,
	                  [DownVotes] [nvarchar](max) NULL,
	                  [AccountId] [nvarchar](max) NULL,
	                  [Age] [nvarchar](max) NULL,
	                  [ProfileImageUrl] [nvarchar](max) NULL)";
      }

      private string CreateBadgesTableSql() {
         return @"CREATE TABLE [dbo].[Badges](
	                  [Id] [int] IDENTITY(1,1) PRIMARY KEY,
	                  [UserId] [int] NULL,
	                  [Name] [nvarchar](max) NULL,
	                  [Date] [datetime] NULL)";
      }

      private string CreateVotesTableSql() {
         return @"CREATE TABLE [dbo].[Votes](
	                  [Id] [int] IDENTITY(1,1) PRIMARY KEY,
	                  [PostId] [int] NULL,
	                  [VoteTypeId] [int] NULL,
	                  [UserId] [int] NULL,
	                  [CreationDate] [datetime] NULL,
	                  [BountyAmount] [nvarchar](max) NULL)";
      }

      private string CreateCommentsTableSql() {
         return @"CREATE TABLE [dbo].[Comments](
	                  [Id] [int] IDENTITY(1,1) PRIMARY KEY,
	                  [PostId] [int] NULL,
	                  [Score] [int] NULL,
	                  [Text] [nvarchar](max) NULL,
	                  [CreationDate] [datetime] NULL,
	                  [UserId] [int] NULL,
	                  [UserDisplayName] [nvarchar](max) NULL)";
      }

      private string CreatePostsTableSql() {
         return @"CREATE TABLE [dbo].[Posts](
	                  [Id] [int] IDENTITY(1,1) PRIMARY KEY,
	                  [PostTypeId] [int] NULL,
	                  [AcceptedAnswerId] [int] NULL,
	                  [CreationDate] [datetime] NULL,
	                  [Score] [int] NULL,
	                  [ViewCount] [int] NULL,
	                  [Body] [nvarchar](max) NULL,
	                  [OwnerUserId] [int] NULL,
	                  [LastEditDate] [datetime] NULL,
	                  [LastActivityDate] [datetime] NULL,
	                  [Title] [nvarchar](max) NULL,
	                  [Tags] [nvarchar](max) NULL,
	                  [AnswerCount] [int] NULL,
	                  [CommentCount] [int] NULL,
	                  [FavoriteCount] [int] NULL,
	                  [ClosedDate] [datetime] NULL,
	                  [CommunityOwnedDate] [datetime] NULL,
	                  [ParentId] [int] NULL,
	                  [OwnerDisplayName] [nvarchar](max) NULL,
	                  [LastEditorDisplayName] [nvarchar](max) NULL)";
      }
   }
}
