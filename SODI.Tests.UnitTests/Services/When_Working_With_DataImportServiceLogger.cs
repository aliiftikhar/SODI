using Moq;
using NUnit.Framework;
using SODI.Contracts;
using SODI.Contracts.Repositories;
using SODI.Contracts.Services;
using SODI.Models;
using SODI.Services;
using System;
using System.IO;
using System.Xml;

namespace SODI.Tests.UnitTests.Services {
   [TestFixture]
   public class When_Working_With_DataImportServiceLogger {

      Mock<IDataImportService<TagEntity>> dataImportService;
      Mock<ILogger> logger;
      IDataImportService<TagEntity> dataImportServiceLogger;

      [SetUp]
      public void Setup() {

         //Arrange
         dataImportService =
            new Mock<IDataImportService<TagEntity>>();

         logger = 
            new Mock<ILogger>();

         dataImportServiceLogger = 
            new DataImportServiceLogger<TagEntity>(logger.Object, dataImportService.Object);
      }

      [Test]
      public void Then_Log_Entry_Should_Be_Made_For_ImportData_Started() {

         //Act
         this.dataImportServiceLogger.ImportData();

         //Assert
         this.logger.Verify(x => x.Log("Import started for: TagEntity"), Times.Once);
      }

      [Test]
      public void Then_Log_Entry_Should_Be_Made_For_ImportData_Completed() {

         //Act
         this.dataImportServiceLogger.ImportData();

         //Assert
         this.logger.Verify(x => x.Log("Import completed for: TagEntity"), Times.Once);
      }
   }
}
