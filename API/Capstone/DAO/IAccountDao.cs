using Capstone.Models;

namespace Capstone.DAO
{
    public interface IAccountDao
    {
        Account CreateAccount(Account account);
        Account GetAccount(int account_id);
        Account GetAccountByUserId(int user_id);
        Account UpdateAccount(Account updatedAccount);
    }
}
