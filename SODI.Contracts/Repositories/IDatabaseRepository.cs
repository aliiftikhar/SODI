using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODI.Contracts.Repositories {
   public interface IDatabaseRepository<T> {
      bool TestConnection();
      void CreateTable();
      void BulkInsert(DataTable dt);
   }
}
