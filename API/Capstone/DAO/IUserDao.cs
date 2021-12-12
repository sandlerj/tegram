using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        UserWithAccountId GetUserWithAccountId(string username);
        User AddUser(string username, string password, string role);
    }
}
