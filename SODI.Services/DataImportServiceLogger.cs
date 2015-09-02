using SODI.Contracts;
using SODI.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SODI.Services {
   public class DataImportServiceLogger<T> : IDataImportService<T> {

      ILogger logger;
      IDataImportService<T> dataImportService;

      public int MaxNumberOfRowsToBeBulkInserted { get; set; }

      public DataImportServiceLogger(ILogger logger, IDataImportService<T> dataImportService) {
         this.logger = logger;
         this.dataImportService = dataImportService;
         this.MaxNumberOfRowsToBeBulkInserted = dataImportService.MaxNumberOfRowsToBeBulkInserted;
      }
      public void ImportData() {

         string stackOverflowEntityTypeName = typeof(T).Name;
         this.logger.Log(String.Format("Import started for: {0}", stackOverflowEntityTypeName));
         
         //Thread.Sleep(5000);
         this.dataImportService.ImportData();

         this.logger.Log(String.Format("Import completed for: {0}", stackOverflowEntityTypeName));
      }      
   }
}
