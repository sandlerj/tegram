using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public class FavoriteSqlPostDao
    {
        private readonly string connectionString;
        public FavoriteSqlPostDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public void AddFavoritePost(int postId, int accountId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO favorited_posts VALUES (@post_id, @account_id);", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
        public Post GetFavoritePost(int postId, int accountId)
        {
            Post returnpost = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT post_id, account_id FROM favorited_posts JOIN posts ON posts.post_id = favorited_posts.post_id WHERE post_id = @post_id AND account_id = @account_id", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnpost = GetFavoritePostFromReader(reader);
                    }
                }
                return returnpost;
            }
            catch
            {
                throw;
            }
        }
        public void RemoveFavoritePost(int postId, int accountId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM favorited_posts WHERE post_id = @post_id AND account_id = @account_id;", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
        private Post GetFavoritePostFromReader(SqlDataReader reader)
        {
            Post post = new Post()
            {
                PostId = Convert.ToInt32(reader["post_id"]),
                AccountId = Convert.ToInt32(reader["account_id"]),
            };
            return post;
        }
    }
}
