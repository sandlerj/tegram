using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IFavoritePostDao
    {
        List<Post> GetListOfFavoritePosts(int postId, int accountId);

        Post GetFavoritePost(int postId, int accountId);

        bool AddFavoritePost(FavoritePost favoritePost);

        bool RemoveFavoritePost(FavoritePost favoritePost); //CHANGED TO BOOL TO MAKE THE CONTROLLER HAPPY.
    }
}
