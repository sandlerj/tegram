using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    //TO ADD BELOW
    /* PostId
     * AccountId
     * MediaLink
     * Caption
     * Timestamp
    */
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
    /// <summary>
    /// How user can comment on posts.
    /// </summary>
    
    /*
    public class CommentPost //Work in the future? Not sure if Lobalu is already making this model or not on his side of working on the DAOs.
    {
        public int CommentId { get; set; }
        public int AccountId { get; set; }
        public int PostId { get; set; }
        public DateTime Timestamp { get; set; } // Would this work just realized that timestamp should be in datetime not string.
        public string Text { get; set; }

    }
    */
}
