using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Comment
    {
        public int AccountId { get; set; }

        public int CommentId { get; set; }

        public int PostId { get; set; }

        public DateTime TimeStamp {get; set; }

        public string Text { get; set; }

    }
}
