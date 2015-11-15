using SODI.Contracts.Models;
using SODI.Contracts.Repositories;
using SODI.Models;
using SODI.Repositories.DatabaseRepositories.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Repositories.DatabaseRepositories {
   public class DatabaseRepositoryStrategy<T> where T : IStackOverflowEntity {

      Dictionary<DatabaseType, IDatabaseRepository<T>> databaseTypeRepositories;
      DatabaseDetails databaseDetails;

      public DatabaseRepositoryStrategy(DatabaseDetails databaseDetails) {

         this.databaseDetails = databaseDetails;

         GetSupportedDatabaseTypeRepositories();
      }

      private void GetSupportedDatabaseTypeRepositories() {
         databaseTypeRepositories = new Dictionary<DatabaseType, IDatabaseRepository<T>>();

         databaseTypeRepositories.Add(DatabaseType.SqlServer, new SqlServerRepository<T>(this.databaseDetails));
      }

      public IDatabaseRepository<T> GetDatabaseRepository() {

         return databaseTypeRepositories[databaseDetails.Type];
      }
   }
}
