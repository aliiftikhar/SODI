using Moq;
using NUnit.Framework;
using SODI.Contracts.Repositories;
using SODI.Contracts.Services;
using SODI.Models;
using SODI.Repositories;
using SODI.Services;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace SODI.Tests.UnitTests.Services {
   [TestFixture]
   public class DataImportServiceTests {

      Mock<IDatabaseRepository<TagEntity>> databaseRepository;
      Mock<IXmlRepository> fileRepository;

      const string tagsXml = "Tags.xml";
      const string badgesXml = "Badges.xml";
      const string commentsXml = "Comments.xml";
      const string postsXml = "Posts.xml";
      const string usersXml = "Users.xml";
      const string votesXml = "Votes.xml";

      const string noFile = "NoFile.xml";

      [SetUp]
      public void Setup() {
         
         string goodDataDirectory =
               Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\TestData\\StackOverflowXmlData\\GoodData\\";

         string goodTagsXml =
            goodDataDirectory + "\\Tags.xml";

         XmlReader tagsXmlReader = XmlReader.Create(goodDataDirectory + tagsXml);
         XmlReader badgesXmlReader = XmlReader.Create(goodDataDirectory + badgesXml);
         XmlReader commentsXmlReader = XmlReader.Create(goodDataDirectory + commentsXml);
         XmlReader postsXmlReader = XmlReader.Create(goodDataDirectory + postsXml);
         XmlReader usersXmlReader = XmlReader.Create(goodDataDirectory + usersXml);
         XmlReader votesXmlReader = XmlReader.Create(goodDataDirectory + votesXml);


         databaseRepository = new Mock<IDatabaseRepository<TagEntity>>();
         databaseRepository.Setup(x => x.TestConnection()).Returns(true);

         fileRepository = new Mock<IXmlRepository>();
         fileRepository.Setup(x => x.FileExist(tagsXml)).Returns(true);
         fileRepository.Setup(x => x.FileExist(badgesXml)).Returns(true);
         fileRepository.Setup(x => x.FileExist(commentsXml)).Returns(true);
         fileRepository.Setup(x => x.FileExist(postsXml)).Returns(true);
         fileRepository.Setup(x => x.FileExist(usersXml)).Returns(true);
         fileRepository.Setup(x => x.FileExist(votesXml)).Returns(true);
         fileRepository.Setup(x => x.FileExist(noFile)).Returns(false);

         fileRepository.Setup(x => x.GetXmlReader(tagsXml)).Returns(tagsXmlReader);
         fileRepository.Setup(x => x.GetXmlReader(badgesXml)).Returns(tagsXmlReader);
         fileRepository.Setup(x => x.GetXmlReader(commentsXml)).Returns(tagsXmlReader);
         fileRepository.Setup(x => x.GetXmlReader(postsXml)).Returns(tagsXmlReader);
         fileRepository.Setup(x => x.GetXmlReader(usersXml)).Returns(tagsXmlReader);
         fileRepository.Setup(x => x.GetXmlReader(votesXml)).Returns(tagsXmlReader);
      }

      [Test]
      public void ImportTagsFromGoodXmlFile() {

         TagEntity entity = new TagEntity();

         IDataImportService<TagEntity> dataImportService =
            new DataImportService<TagEntity>(databaseRepository.Object, fileRepository.Object, entity);

         dataImportService.ImportData();

      }
   }
}
