using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IPostDao
    {
        Post GetPost(int postId);

        void UploadPost(int postId, string MediaLink);

        void LikePost(int postId);

        void UnlikePost(int postId);

        Post GetFavoritePost(int postId); //Don't need 'bool favorite' when getting favorites?

        void AddFavoritePost(int postId);

        void RemoveFavoritePost(int postId);
    }
}
