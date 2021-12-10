using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class AccountSqlDao: IAccountDao
    {
        private readonly string connectionString;

        public AccountSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Account CreateAccount(Account account)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO accounts (user_id, email, profile_image) VALUES (@user_id, @email, @profile_image)", conn);
                    cmd.Parameters.AddWithValue("@user_id", account.UserId);
                    cmd.Parameters.AddWithValue("@email", account.Email);
                    cmd.Parameters.AddWithValue("@profile_image", account.ProfileImage);
                    int newAccountId = Convert.ToInt32(cmd.ExecuteScalar());

                    account.AccountId = newAccountId;
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return account;
        }

        public Account GetAccount(int account_id)
        {
            Account account = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT accounts.account_id, accounts.user_id, accounts.email, accounts.profile_image, users.username " +
                        "FROM accounts JOIN users on accounts.user_id = users.user_id WHERE account_id = @account_id", conn);
                    cmd.Parameters.AddWithValue("@account_id", account_id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        account = GetAccountFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return account;
        }

        public Account GetAccountByUserId(int user_id)
        {
            Account account = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT accounts.account_id, accounts.user_id, accounts.email, accounts.profile_image, users.username " +
                        "FROM accounts JOIN users on accounts.user_id = users.user_id WHERE accounts.user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        account = GetAccountFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return account;
        }
        public List<Account> GetAllAccounts()
        {
            List<Account> allAccounts = new List<Account>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT accounts.account_id, accounts.user_id, accounts.email, accounts.profile_image, users.username FROM accounts JOIN users on accounts.user_id = users.user_id", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Account account = GetAccountFromReader(reader);
                        allAccounts.Add(account);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return allAccounts;
        }
        public Account UpdateAccount(Account updatedAccount)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE accounts SET user_id = @user_id, email = @email, profile_pic = @profile_pic WHERE transfer_id = @transfer_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", updatedAccount.UserId);
                    cmd.Parameters.AddWithValue("@email", updatedAccount.Email);
                    cmd.Parameters.AddWithValue("@profile_image", updatedAccount.ProfileImage);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return updatedAccount;
        }
        private Account GetAccountFromReader(SqlDataReader reader)
        {
            Account account = new Account()
            {
                AccountId = Convert.ToInt32(reader["account_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                Email = Convert.ToString(reader["email"]),
                ProfileImage = Convert.ToString(reader["profile_image"]),
                Username = Convert.ToString(reader["username"])
            };

            return account;
        }
    }
}
