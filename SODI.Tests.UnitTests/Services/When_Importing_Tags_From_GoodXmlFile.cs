using Moq;
using NUnit.Framework;
using SODI.Contracts.Repositories;
using SODI.Contracts.Services;
using SODI.Models;
using SODI.Services;
using System;
using System.IO;
using System.Xml;

namespace SODI.Tests.UnitTests.Services {
   
   [TestFixture]
   public class When_Importing_Tags_From_GoodXmlFile {

      Mock<IDatabaseRepository<TagEntity>> databaseRepository;
      Mock<IXmlRepository> fileRepository;

      const string tagsXml = "Tags.xml";

      [SetUp]
      public void Setup() {

         string goodDataDirectory =
            Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\TestData\\StackOverflowXmlData\\GoodData\\";

         XmlReader tagsXmlReader = XmlReader.Create(goodDataDirectory + tagsXml);
         
         databaseRepository = new Mock<IDatabaseRepository<TagEntity>>();
         databaseRepository.Setup(x => x.TestConnection()).Returns(true);

         fileRepository = new Mock<IXmlRepository>();
         fileRepository.Setup(x => x.FileExist(It.IsAny<string>())).Returns(true);
         fileRepository.Setup(x => x.GetXmlReader(It.IsAny<string>())).Returns(tagsXmlReader);
      }

      [Test]
      public void Then_No_Exception_Should_Be_Thrown() {

         TagEntity entity = new TagEntity();

         IDataImportService<TagEntity> dataImportService =
            new DataImportService<TagEntity>(databaseRepository.Object, fileRepository.Object, entity);

         dataImportService.ImportData();
      }

      [Test]
      public void Then_FileExist_In_XmlRepository_Should_Get_Called_Once() {

      }

      [Test]
      public void Then_FileNotFoundException_Should_Be_Thrown_If_No_XML_File_Found() {

      }

      [Test]
      public void Then_TestConnection_In_DatabaseRepository_Should_Get_Called_Once() {

      }

      [Test]
      public void Then_DatabaseConnectionTestFailedException_Should_Be_Thrown_If_TestConnection_Failed() {

      }

      [Test]
      public void Then_CreateTable_In_DatabaseRepository_Should_Get_Called_Once() {

      }

      [Test]
      public void Then_BulkInsert_In_DatabaseRepository_Should_Get_Called_3_Times() {

      }
   }
}
