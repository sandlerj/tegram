using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountDao accountDao;
        private readonly IPostDao postDao;
        private readonly System.Security.Cryptography.MD5 md5 =
            System.Security.Cryptography.MD5.Create();
        public AccountsController(IAccountDao _accountDao, IPostDao _postDao)
        {
            accountDao = _accountDao;
            postDao = _postDao;
        }


        [HttpGet]
        public ActionResult<List<Account>> GetAllAccounts()
        {
            List<Account> allAccounts = accountDao.GetAllAccounts();
            return allAccounts;
        }

        [HttpGet("{accountId}")]
        public ActionResult<Account> GetAccount(int accountId) {
            Account account = accountDao.GetAccount(accountId);
            return account;
        }

        [HttpGet("{accountId}/details")]
        public ActionResult<AccountDetails> GetPostDetails(int accountId)
        {
            AccountDetails accountDetails = accountDao.GetAccountDetails(accountId);
            return accountDetails;
        }

        [HttpGet("{accountId}/posts")]
        public ActionResult<List<Post>> GetPostsByAccountId(int accountId)
        {
            List<Post> accountPosts = postDao.GetListOfPostsByAccountId(accountId);
            return accountPosts;
        }

        [HttpPost("{accountId}/posts")]
        public ActionResult<Post> CreatePost(int accountId, Post post) {
            Post createdPost = postDao.UploadPost(post);
            return Created($"/{accountId}/{createdPost.PostId}", createdPost);
        }

        [HttpPut("{accountId}")]
        public ActionResult<Account> UpdateAccount(Account updatedAccount)
        {
            if (string.IsNullOrEmpty(updatedAccount.ProfileImage) ||
                updatedAccount.ProfileImage.Contains("gravatar")) // if already set as gravatar, hash needs reset
            {
                string profileImg = GetGravaterString(updatedAccount.Email);
                updatedAccount.ProfileImage = profileImg;
            }
            Account account = accountDao.UpdateAccount(updatedAccount);
            return account;
        }
        private string GetGravaterString(string email)
        {
            string input = email.Trim().ToLower();

            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            string outputStr = $"https://gravatar.com/avatar/{ hash.ToLower() }?d=identicon";

            return outputStr;
        }
    }
}
