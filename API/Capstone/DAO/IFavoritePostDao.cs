using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IFavoritePostDao
    {
        Post GetFavoritePost(int postId, int accountId);

        void AddFavoritePost(int postId, int accountId);

        void RemoveFavoritePost(int postId, int accountId);
    }
}
