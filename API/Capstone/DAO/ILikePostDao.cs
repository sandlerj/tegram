using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ILikePostDao
    {
        List<int> LikePost(LikePost likePost);

        bool UnlikePost(LikePost likePost);

        List<int> GetAccountIdsLikingPost(int postId);
    }
}
