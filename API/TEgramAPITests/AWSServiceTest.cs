using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

using Capstone.Services;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TEgramAPITests
{
    [TestClass]
    public class AWSServiceTest
    {
        public IFileStorageService storageService = new AWSS3FileStorage();

        [TestMethod]
        public void TestSomething()
        {
            string imgName = "rain.jpg";
            string debugDir = Directory.GetParent(Environment.CurrentDirectory).ToString();
            Directory.SetCurrentDirectory("..\\..\\..\\TestData");

            //string imgPath = Path.Combine(debugDir, @"TestData\", imgName);
            string imgPath = Path.Combine(Directory.GetCurrentDirectory(), imgName);


            Console.WriteLine($"Attemping to upload {imgPath} to storage service...");
            string result;
            using (var stream = File.OpenRead(imgPath))
            {
                var formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                result = storageService.UploadFileToStorage(formFile);
            }
            Console.WriteLine(result);
        }
    }
}


/*
 From stack overflow - https://stackoverflow.com/questions/51704805/how-to-instantiate-an-instance-of-formfile-in-c-sharp-without-moq

using (var stream = File.OpenRead("placeholder.pdf"))
{
    var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
    {
        Headers = new HeaderDictionary(),
        ContentType = "application/pdf"
    };
}
 
 */