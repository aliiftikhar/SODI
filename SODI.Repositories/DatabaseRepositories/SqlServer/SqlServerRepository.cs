using SODI.Contracts.Models;
using SODI.Contracts.Repositories;
using SODI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Repositories.DatabaseRepositories.SqlServer {
   public class SqlServerRepository<T> : IDatabaseRepository<T>
   where T : IStackOverflowEntity {

      private readonly string connectionString;
      private readonly string masterConnectionString;

      private DatabaseDetails dbConnectionDetails;

      public SqlServerRepository(DatabaseDetails dbConnectionDetails) {

         this.dbConnectionDetails = dbConnectionDetails;

         this.connectionString =
               String.Format("Server={0};Database={1};User Id={2};Password={3};",
                  dbConnectionDetails.Server,
                  dbConnectionDetails.DatabaseName,
                  dbConnectionDetails.Username,
                  dbConnectionDetails.Password);

         this.masterConnectionString =
               String.Format("Server={0};Database={1};User Id={2};Password={3};",
               dbConnectionDetails.Server,
               "master",
               dbConnectionDetails.Username,
               dbConnectionDetails.Password);
      }

      public bool TestConnection() {
         string result = string.Empty;

         result = ExecuteScalarSql("Select 1");

         return String.Equals(result, "1");
      }

      public void CreateTable() {

         string createTableSql =
           new CreateTableStrategy().GetCreateTableSql(typeof(T));

         ExecuteNonQuerySql(createTableSql);
      }

      public void BulkInsert(DataTable dt) {
         using (
         SqlBulkCopy bulkInsert = new SqlBulkCopy(this.connectionString)) {
            bulkInsert.DestinationTableName = dt.TableName;
            bulkInsert.WriteToServer(dt);
         }
      }

      #region PRIVATE METHODS
      private void ExecuteNonQuerySql(String sql) {
         ExecuteNonQuerySql(sql, connectionString);
      }

      private void ExecuteNonQuerySql(String sql, string connectionString) {
         using (SqlConnection connection = new SqlConnection(connectionString)) {
            connection.Open();

            using (SqlCommand cmd = new SqlCommand(sql, connection)) {
               cmd.ExecuteNonQuery();
            }

            connection.Close();
         }
      }

      private string ExecuteScalarSql(String sql) {

         string result = string.Empty;

         using (SqlConnection connection = new SqlConnection(connectionString)) {
            connection.Open();

            using (SqlCommand cmd = new SqlCommand(sql, connection)) {
               result = cmd.ExecuteScalar().ToString();
            }

            connection.Close();
         }

         return result;
      }

      #endregion

      public void CreateDatabaseIfAlreadyNotCreated() {
         
         string sql = String.Format(@"IF (Not EXISTS (SELECT name 
                                    FROM master.dbo.sysdatabases 
                                    WHERE ('[' + name + ']' = '[{0}]'
                                    OR name = '[{0}]')))
                                    CREATE DATABASE [{0}]", this.dbConnectionDetails.DatabaseName);

         ExecuteNonQuerySql(sql, this.masterConnectionString);
      }
   }
}
