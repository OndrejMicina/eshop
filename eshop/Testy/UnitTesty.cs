using eshop.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace eshop.Testy
{
    public class UnitTesty
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        [Fact]
        public void FileUploadCorrectTest()
        {

            //Arrange 
            var fileMock = new Mock<IFormFile>();
            var content = "Content";
            var fileName = "test.jpg";
            var contentType = "Image";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.ContentType).Returns(contentType);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var iFormFileFake = fileMock.Object;

            FileUpload upload = new FileUpload("C:\\Users\\Ondrej\\source\\repos\\OndrejMicina\\eshop\\eshop\\wwwroot", "Tests", "image");

            //Act
            var result = upload.CheckFileContent(iFormFileFake);
            //ocakava false
            Assert.True(result);
        }


        [Fact]
        public void FileUploadIncorrectTest()
        {

            //Arrange 
            var fileMock = new Mock<IFormFile>();
            var content = "Content";
            var fileName = "test.pdf";
            var contentType = "Pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.ContentType).Returns(contentType);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var iFormFileFake = fileMock.Object;

            FileUpload upload = new FileUpload("C:\\Users\\Ondrej\\source\\repos\\OndrejMicina\\eshop\\eshop\\wwwroot", "Tests", "image");

            //Act
            var result = upload.CheckFileContent(iFormFileFake);
            //ocakava false
            Assert.False(result);
        }
    }
}
