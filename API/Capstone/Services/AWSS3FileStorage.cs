using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

// To interact with AWS S3
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.IO;
using Amazon.Runtime;

namespace Capstone.Services
{
    public class AWSS3FileStorage : IFileStorageService
    {
        // creds and stuff will need to go in some sort of hidden text file
        private const string bucketName = "tegram-delicious-circus"; //should it just be the last bit?


        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast2;
        private static IAmazonS3 s3Client;
        private string objectUrl;

        public AWSS3FileStorage()
        {
            s3Client = new AmazonS3Client(bucketRegion);
            objectUrl = $"https://{bucketName}.s3.{bucketRegion.SystemName}.amazonaws.com/";
        }


    public string UploadFileToStorage(IFormFile formFile)
        {
            // call UploadFile

            return objectUrl;
        }
        private async Task UploadFile(IFormFile formFile)
        {
            //honestly if we didn't mind making a separate post and then put request to the DB table
            // we could use the postID as the key for s3, only issue being if a user replaces a photo which honestly shouldn't be possible....
    
            string fileKey = GenerateFileKey(formFile.FileName);
            objectUrl += fileKey;

            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);

                using (var fileStreamToUpload = formFile.OpenReadStream())
                {
                    await fileTransferUtility.UploadAsync(fileStreamToUpload, bucketName, fileKey);
                }
            }
            catch(AmazonS3Exception e)
            {
                Console.WriteLine($"Error occured when accessing AWS Service: {e.Message}");
                throw e;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error occured: {e.Message}");
                throw e;
            }
        }

        private string GenerateFileKey(string fileName)
        {
            string result;
            Random rand = new Random();

            string fileExtension = Path.GetExtension(fileName);

            while (true) // need to keep generating key until unique
            {
                //hash rand byte[] with MD5
                // using md5 bc fast-ish, security not needed, collisions okay but not wanted
                using (var md5Hash = MD5.Create())
                {
                    byte[] randBytes = new byte[10];
                    rand.NextBytes(randBytes);
                    byte[] hashBytes = md5Hash.ComputeHash(randBytes);
                    string hashStr = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
                    result = hashStr + fileExtension;
                }

                // doublecheck that key doesn't already exist (get object should return nothing)

                try
                {
                    // try to get the object at s3://bucket/result, if found, regen to ensure unique
                }
                catch (AmazonServiceException e)
                {
                    if (e.ErrorCode == "NoSuchKey")
                    {
                        // if we don't hit this, the key already exists/there's another issue
                        break;
                    }
                    else
                    {
                        // could have been a dif error, throw up
                        throw e;
                    }
                }
            }

            // return string name



            throw new NotImplementedException();
        }
    }
}
