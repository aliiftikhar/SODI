using SODI.Contracts.Models;
using SODI.Contracts.Repositories;
using SODI.Contracts.Services;
using SODI.Models;
using SODI.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SODI.Services {
   public class DataImportService<T> : IDataImportService<T>
      where T : IStackOverflowEntity {

      public int MaxNumberOfRowsToBeBulkInserted { get; set; }

      IDatabaseRepository<T> entityRepository;
      IXmlRepository xmlRepository;
      IStackOverflowEntity entity;
      public DataImportService(IDatabaseRepository<T> databaseRepository, IXmlRepository fileRepository, T entity) {
         this.entityRepository = databaseRepository;
         this.xmlRepository = fileRepository;
         this.entity = entity;
         this.MaxNumberOfRowsToBeBulkInserted = 5000;
      }

      public void ImportData() {

         ValidateEntity();

         ValidateFilePath();

         this.entityRepository.CreateDatabaseIfAlreadyNotCreated();

         TestDatabseConnection();

         this.entityRepository.CreateTable();

         InsertDataFromXmlIntoDatabase();
      }

      #region PRIVATE METHODS

      private void ValidateEntity() {

         string entityName  =
            typeof(T).Name;

         if (String.IsNullOrEmpty(this.entity.GetEntityName()))
            throw new Exception("Unable to reterive valid EntityName for: " + entityName);

         if (String.IsNullOrEmpty(this.entity.GetXmlFileName()))
            throw new Exception("Unable to reterive valid XmlFileName for: " + entityName);

         if (this.entity.GetXmlAttributes() == null)
            throw new Exception("Unable to reterive valid XmlAttributes for: " + entityName);
      }

      private void ValidateFilePath() {

         string entityName =
            typeof(T).Name;

         string xmlFileName =
            this.entity.GetXmlFileName();

         if (!this.xmlRepository.FileExist(xmlFileName))
            throw new Exception("XML File not found for: " + entityName);
      }

      private void TestDatabseConnection() {

         bool testPassed = false;
         string exceptionMessage = string.Empty;

         try {
            testPassed = this.entityRepository.TestConnection();
         }
         catch (Exception ex) {
            exceptionMessage = ex.Message;
         }

         if(!testPassed)
            throw new Exception(String.Format("Database Connection Test failed. Error: {0}", exceptionMessage));
      }

      private void InsertDataFromXmlIntoDatabase() {

         string xmlFileName =
               this.entity.GetXmlFileName();

         DataTable dt = null;

         using (XmlReader reader = this.xmlRepository.GetXmlReader(xmlFileName)) {
            while (reader.Read()) {
               if (reader.IsStartElement()) {
                  if (reader.Name == "row") {

                     if (dt == null)
                        dt = CreateDataTable();

                     CreateRows(dt, reader);

                     if (dt.Rows.Count % this.MaxNumberOfRowsToBeBulkInserted == 0) {
                        this.entityRepository.BulkInsert(dt);
                        dt.Clear(); dt.Dispose(); dt = null;
                     }
                  }
               }
            }
         }

         if (dt != null && dt.Rows.Count > 0) {
            this.entityRepository.BulkInsert(dt);
            dt.Clear(); dt.Dispose(); dt = null;
         }
      }

      private DataTable CreateDataTable() {

         string tableName =
            this.entity.GetEntityName();

         List<string> columnNames =
              this.entity.GetXmlAttributes();

         DataTable dt =
            new DataTable(tableName);

         foreach (string columnName in columnNames) {
            dt.Columns.Add(columnName);
         }

         return dt;
      }

      private void CreateRows(DataTable dt, XmlReader reader) {

         List<string> attributes =
            this.entity.GetXmlAttributes();

         DataRow dr = dt.NewRow();
         foreach (string attribute in attributes) {
            dr[attribute] = reader[attribute];
         }

         dt.Rows.Add(dr);
      }

      #endregion

   }
}
