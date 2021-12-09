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

        bool AddFavoritePost(int postId, int accountId);

        bool RemoveFavoritePost(int postId, int accountId); //CHANGED TO BOOL TO MAKE THE CONTROLLER HAPPY.
    }
}
