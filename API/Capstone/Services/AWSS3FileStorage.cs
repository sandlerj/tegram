using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

// To interact with AWS S3
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;


namespace Capstone.Services
{
    public class AWSS3FileStorage : IFileStorageService
    {
        // creds and stuff will need to go in some sort of hidden text file
        private const string bucketName = "tegram-delicious-circus"; //should it just be the last bit?


        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast2;
        private static IAmazonS3 s3Client;
        private readonly string objectUrl;


        // todo - Not this. Get this out of the csw
        private string awsAccessId;  // /bin/Credentials/tegram-delicious-circus-credentials.csv
        private string awsSecret;
        private readonly string FULLY_QUALIFIED_CSV_PATH = "C:\\Users\\Student\\workspace\\capstones\\c-final-capstone-team-3\\API\\Credentials\\tegram-delicious-circus-credentials.csv";

        public AWSS3FileStorage()
        {

            GetCredsFromCSV(FULLY_QUALIFIED_CSV_PATH);
            //set up credentials (this should reeeeally be set up through a dependency injection... gotta figure that out
            AWSCredentials credentials = new BasicAWSCredentials(awsAccessId, awsSecret);

            s3Client = new AmazonS3Client(credentials, bucketRegion);
            objectUrl = $"https://{bucketName}.s3.{bucketRegion.SystemName}.amazonaws.com/";
        }

        private void GetCredsFromCSV(string csvPath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(csvPath))
                {
                    // csv should contain exactly two lines, with accessId and secret on the second line as the 3rd and 4th entries respectively (as downloaded from aws)
                    reader.ReadLine();
                    string credLine = reader.ReadLine();
                    string[] credArr = credLine.Split(',');
                    awsAccessId = credArr[2];
                    awsSecret = credArr[3];
                }
            } 
            catch (Exception e)
            {
                throw e;
            }
        }

        public string UploadFileToStorage(IFormFile formFile)
        {
            // call UploadFile
            string resultStr = UploadFile(formFile).Result;
            // todo - is there a way this whole thing can be async? maybe that's something I can go back and adjust...
            // Actually, I don't think it's needed - the upload is done asynchronously, so the actualy result string should be available to return

            return resultStr;
        }
        private async Task<string> UploadFile(IFormFile formFile)
        {
            //honestly if we didn't mind making a separate post and then put request to the DB table
            // we could use the postID as the key for s3, only issue being if a user replaces a photo which honestly shouldn't be possible....
    
            string fileKey = GenerateFileKey(formFile.FileName);

            try
            {
                var fileTransferUtility = new TransferUtility(s3Client); 

                using (var fileStreamToUpload = formFile.OpenReadStream())
                {
                    await fileTransferUtility.UploadAsync(fileStreamToUpload, bucketName, fileKey); // i THINK this is happening on a separate thread, and the rest can continue
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
            return objectUrl + fileKey;
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
                    // since security doesn't matter, and collision isn't likely on 10bytes
                    byte[] randBytes = new byte[10];
                    rand.NextBytes(randBytes);
                    string hashStr = BitConverter.ToString(randBytes).Replace("-", string.Empty);

                    //byte[] hashBytes = md5Hash.ComputeHash(randBytes);
                    //string hashStr = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                    result = hashStr + fileExtension;
                }

                // doublecheck that key doesn't already exist (get object should return nothing)

                try
                {
                    // try to get the object at s3://bucket/result, if found, regen to ensure unique
                    s3Client.GetObjectAsync(bucketName, result).Wait(); // should throw a 404 NoSuchKey error, i.e. the key is unused
                    // method invoked synchronously because it needs to be, hence no async/await here
                    //if (res.Status != TaskStatus.Created)
                    //{
                    //    throw new Exception($"Error occurring within AWS service: {res.Status}");
                    //}

                    
                }
                catch (AggregateException e)
                {
                    try
                    {
                        throw e.InnerException;
                    }
                    catch (AmazonS3Exception e1)
                    {
                        break;
                    }
                    catch (Exception e1)
                    {
                        throw e1;
                    }
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
                } catch (Exception e)
                {
                    throw e;
                }
                
            }

            // return string name


            return result;
        }
    }
}
