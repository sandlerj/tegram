using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ILikePostDao
    {
        bool LikePost(LikePost likePost);

        bool UnlikePost(LikePost likePost);
    }
}
