using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IAccountDao
    {
        Account CreateAccount(Account account);
        Account GetAccount(int account_id);
        Account GetAccountByUserId(int user_id);
        List<Account> GetAllAccounts();
        Account UpdateAccount(Account updatedAccount);
    }
}
