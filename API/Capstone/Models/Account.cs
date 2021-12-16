using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Account
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public string Bio { get; set; }
        public DateTime MemberSince { get; set; }
        public string Username { get; set; } = null;
    }
    public class AccountDetails
    {
        public int NumberOfPosts { get; set; }
        public int NumberOfLikesGiven { get; set; }
        public int NumberOfLikesReceived { get; set; }
    }
}
