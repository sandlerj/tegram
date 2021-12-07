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
        public string Timestamp { get; set; }

    }
    /// <summary>
    /// If a user requests to view one post.
    /// </summary>
    public class ReturnPost
    {
        public int PostId { get; set; }
        public string MediaLink { get; set; }
        public string Caption { get; set; }
        public string Timestamp { get; set; }

    }
    /// <summary>
    /// How user can favorite posts they like.
    /// </summary>
    public class FavoritePost //Work in the future
    {
        public int PostId { get; set; }

        //public bool favorite {get; set;}?
    }
    /// <summary>
    /// How user can comment on posts.
    /// </summary>
    public class CommentPost //Work in the future?
    {
        public int PostId { get; set; }
    }
}
