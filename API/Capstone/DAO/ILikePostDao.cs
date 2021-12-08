using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ILikePostDao
    {
        Post GetLikedPost(int postId, int accountId);

        Post LikePost(int postId, int accountId);

        bool UnlikePost(int postId, int accountId); //CHANGED THIS TO BOOL BECAUSE POST CONTROLLER DIDN'T LIKE IT AT FIRST.
    }
}
