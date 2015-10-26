using SODI.Contracts;
using SODI.Contracts.Models;
using SODI.Contracts.Repositories;
using SODI.Contracts.Services;
using SODI.Models;
using SODI.Repositories;
using SODI.Repositories.DatabaseRepositories;
using SODI.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Console {
   class Program {
      static void Main(string[] args) {

         string xmlSourceFolderPathIncludingLastSlash =
            ConfigurationManager.AppSettings["XmlSourceFolderPathIncludingLastSlash"];

         IXmlRepository xmlRepository =
            new XmlRepository(xmlSourceFolderPathIncludingLastSlash);

         DatabaseDetails databaseDetails =
            GetDatabaseDetailsFromAppConfig();

         Logger logger =
            new Logger();

         ImportStackOverflowEntities(xmlRepository, databaseDetails, logger);

         logger.Log("All Done");
      }
      private static DatabaseDetails GetDatabaseDetailsFromAppConfig() {

         DatabaseDetails databaseConnectionDetails =
            new DatabaseDetails();

         databaseConnectionDetails.Type =
            (DatabaseType)Enum.Parse(typeof(DatabaseType), ConfigurationManager.AppSettings["DatabaseType"]);

         databaseConnectionDetails.Server =
            ConfigurationManager.AppSettings["DatabaseServerName"];

         databaseConnectionDetails.DatabaseName =
            ConfigurationManager.AppSettings["DatabaseName"];

         databaseConnectionDetails.Username =
            ConfigurationManager.AppSettings["DatabaseUsername"];

         databaseConnectionDetails.Password =
            ConfigurationManager.AppSettings["DatabasePassword"];

         return databaseConnectionDetails;
      }

      private static void ImportStackOverflowEntities(IXmlRepository xmlRepository, DatabaseDetails databaseDetails, Logger logger) {

         Import<TagEntity>(xmlRepository, databaseDetails, logger);

         Import<UserEntity>(xmlRepository, databaseDetails, logger);

         Import<BadgeEntity>(xmlRepository, databaseDetails, logger);

         Import<VoteEntity>(xmlRepository, databaseDetails, logger);

         Import<CommentEntity>(xmlRepository, databaseDetails, logger);

         Import<PostEntity>(xmlRepository, databaseDetails, logger);
      }

      private static void Import<T>(IXmlRepository xmlRepository, DatabaseDetails databaseConnectionDetails, Logger logger)
         where T : IStackOverflowEntity, new() {

         IDatabaseRepository<T> repository =
               new DatabaseRepositoryStrategy<T>(databaseConnectionDetails).GetDatabaseRepository();

         T entity =
            new T();

         IDataImportService<T> dataImportService =
               new DataImportServiceLogger<T>(logger,
                  new DataImportService<T>(repository, xmlRepository, entity));

         dataImportService.ImportData();
      }
   }
}
