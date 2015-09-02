using Moq;
using NUnit.Framework;
using SODI.Contracts.Models;
using SODI.Contracts.Repositories;
using SODI.Contracts.Services;
using SODI.Models;
using SODI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;

namespace SODI.Tests.UnitTests.Services {

   [TestFixture]
   public class When_Working_With_DataImportService {

      Mock<IDatabaseRepository<TagEntity>> databaseRepository;
      Mock<IXmlRepository> fileRepository;
      IDataImportService<TagEntity> dataImportService;
      Mock<TagEntity> entity;
      string testXml;

      [SetUp]
      public void Setup() {

         MockStackOverflowEntity();
         MockDatabaseRepository();
         MockXmlRepository();

         this.dataImportService =
            new DataImportService<TagEntity>(databaseRepository.Object, fileRepository.Object, entity.Object);
      }

      [Test]
      public void Then_No_Exception_Should_Be_Thrown() {

         //Act, Assert
         Assert.DoesNotThrow(() => this.dataImportService.ImportData());
      }

      [Test]
      public void Then_FileExist_In_XmlRepository_Should_Get_Called_Once() {

         //Act
         this.dataImportService.ImportData();

         //Assert
         this.fileRepository.Verify(x => x.FileExist(It.IsAny<string>()), Times.Once);
      }

      [Test]
      [ExpectedException(ExpectedException = typeof(Exception), ExpectedMessage = "XML File not found for: TagEntity")]
      public void Then_FileNotFoundException_Should_Be_Thrown_If_No_XML_File_Found() {

         //Arrange
         this.fileRepository.Setup(x => x.FileExist(It.IsAny<string>())).Returns(false);

         //Act
         this.dataImportService.ImportData();
      }

      [Test]
      public void Then_TestConnection_In_DatabaseRepository_Should_Get_Called_Once() {
         
         //Act
         this.dataImportService.ImportData();

         //Assert
         this.databaseRepository.Verify(x=>x.TestConnection(), Times.Once);
      }

      [Test]
      [ExpectedException(ExpectedException = typeof(Exception), ExpectedMessage = "Database Connection Test failed. Error: ")]
      public void Then_DatabaseConnectionTestFailedException_Should_Be_Thrown_If_TestConnection_Failed() {

         //Arrange:
         this.databaseRepository.Setup(x => x.TestConnection()).Returns(false);

         //Act, Assert
         this.dataImportService.ImportData();
      }

      [Test]
      [ExpectedException(ExpectedException = typeof(Exception), ExpectedMessage = "Database Connection Test failed. Error: Something bad happened")]
      public void Then_DatabaseConnectionTestFailedException_Should_Be_Thrown_If_Exception_In_TestConnection() {

         //Arrange:
         databaseRepository.Setup(x => x.TestConnection()).Throws(new Exception("Something bad happened"));

         //Act
         this.dataImportService.ImportData();
      }

      [Test]
      public void Then_CreateTable_In_DatabaseRepository_Should_Get_Called_Once() {

         //Act:
         this.dataImportService.ImportData();

         //Assert:
         this.databaseRepository.Verify(x=>x.CreateTable(),Times.Once);

      }

      [Test]
      public void Then_BulkInsert_In_DatabaseRepository_Should_Get_Called_4_Times() {

         //Arrange
         this.dataImportService.MaxNumberOfRowsToBeBulkInserted = 2;

         //Act:
         this.dataImportService.ImportData();

         //Assert:
         this.databaseRepository.Verify(x=>x.BulkInsert(It.IsAny<DataTable>()), Times.Exactly(4));

      }

      [Test]
      [ExpectedException(ExpectedException = typeof(Exception), ExpectedMessage = "Unable to reterive valid EntityName for: TagEntity")]
      public void Then_ValidateEntity_Should_Throw_Exception_If_EntityName_Is_Missing() {
         
         //Arrange
         entity.Setup(x=>x.GetEntityName()).Returns(string.Empty);

         dataImportService = 
            new DataImportService<TagEntity>(databaseRepository.Object, fileRepository.Object, entity.Object);

         //Act
         dataImportService.ImportData();
         
      }

      [Test]
      [ExpectedException(ExpectedException = typeof(Exception), ExpectedMessage = "Unable to reterive valid XmlFileName for: TagEntity")]
      public void Then_ValidateEntity_Should_Throw_Exception_If_EntityXmlFileName_Is_Missing() {

         //Arrange
         entity.Setup(x => x.GetXmlFileName()).Returns(string.Empty);

         dataImportService =
            new DataImportService<TagEntity>(databaseRepository.Object, fileRepository.Object, entity.Object);

         //Act
         dataImportService.ImportData();
      }

      [Test]
      [ExpectedException(ExpectedException = typeof(Exception), ExpectedMessage = "Unable to reterive valid XmlAttributes for: TagEntity")]
      public void Then_ValidateEntity_Should_Throw_Exception_If_EntityXmlAttributes_Is_Null() {

         //Arrange
         entity.Setup(x => x.GetXmlAttributes()).Returns((List<string>)null);

         dataImportService =
            new DataImportService<TagEntity>(databaseRepository.Object, fileRepository.Object, entity.Object);

         //Act
         dataImportService.ImportData();
      }

      #region PRIVATE METHODS
      private void MockXmlRepository() {
         testXml =
            Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) +
            "\\TestData\\StackOverflowXmlData\\GoodData\\Tags.xml";

         XmlReader tagsXmlReader =
            XmlReader.Create(testXml);

         fileRepository =
            new Mock<IXmlRepository>();
         fileRepository.Setup(x => x.FileExist(It.IsAny<string>())).Returns(true);
         fileRepository.Setup(x => x.GetXmlReader(It.IsAny<string>())).Returns(tagsXmlReader);
      }

      private void MockDatabaseRepository() {
         databaseRepository =
            new Mock<IDatabaseRepository<TagEntity>>();
         databaseRepository.Setup(x => x.TestConnection()).Returns(true);
      }

      private void MockStackOverflowEntity() {
         entity =
            new Mock<TagEntity>();
         entity.Setup(x => x.GetEntityName()).Returns("TestEntity");
         entity.Setup(x => x.GetXmlFileName()).Returns("TestEntity.xml");
         entity.Setup(x => x.GetXmlAttributes()).Returns(new List<string>(){"one", "two"});
      }
      #endregion
   }

}
