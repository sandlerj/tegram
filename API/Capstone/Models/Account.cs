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
    }
}
