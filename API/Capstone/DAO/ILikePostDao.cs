using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ILikePostDao
    {
        void LikePost(int postId, int accountId);

        void UnlikePost(int postId, int accountId);
    }
}
