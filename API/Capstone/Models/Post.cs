﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    
    /// <summary>
    /// Offical post model.
    /// </summary>
    public class Post
    {
        public int PostId { get; set; }
        public int AccountId { get; set; }
        public string MediaLink { get; set; }
        public string Caption { get; set; }
        public DateTime Timestamp { get; set; }

    }
    /// <summary>
    /// If a user requests to view one post.
    /// </summary>
    public class ReturnPost
    {
        public int PostId { get; set; }
        public string MediaLink { get; set; }
        public string Caption { get; set; }
        public DateTime Timestamp { get; set; }

    }
    /// <summary>
    /// How user can favorite posts they like.
    /// </summary>
    public class FavoritePost //Work in the future
    {
        public int PostId { get; set; }

        public int AccountId {get; set;}
    }
    /// <summary>
    /// How user can like posts.
    /// </summary>
    public class LikePost
    {
        public int PostId { get; set; }
        public int AccountId { get; set; }
    }
    
}
