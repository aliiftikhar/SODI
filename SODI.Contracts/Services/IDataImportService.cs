using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Contracts.Services {
   public interface IDataImportService<T> {

      int MaxNumberOfRowsToBeBulkInserted { get; set; }
      void ImportData();
   }
}
