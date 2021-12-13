using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public class LikeSqlPostDao : ILikePostDao
    {
        private readonly string connectionString;
        public LikeSqlPostDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<int> GetAccountIdsLikingPost(int postId)
        {
            List<int> accountIds = new List<int>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string queryString = "SELECT account_id FROM liked_posts WHERE post_id = @postId";
                    SqlCommand cmd = new SqlCommand(queryString, conn);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        accountIds.Add(Convert.ToInt32(reader["account_id"]));
                    }
                }
                return accountIds;
            }
            catch (SqlException e)
            {

                throw e;
            }
        }

        public List<int> LikePost(LikePost likePost)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO liked_posts VALUES (@account_id, @post_id);", conn);
                    cmd.Parameters.AddWithValue("@account_id", likePost.AccountId);
                    cmd.Parameters.AddWithValue("@post_id", likePost.PostId);
                    int result = cmd.ExecuteNonQuery();

                    return GetAccountIdsLikingPost(likePost.PostId);
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool UnlikePost(LikePost likePost)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM liked_posts WHERE post_id = @post_id AND account_Id = @account_id", conn);
                    cmd.Parameters.AddWithValue("@post_id", likePost.PostId);
                    cmd.Parameters.AddWithValue("@account_id", likePost.AccountId);
                    int result = cmd.ExecuteNonQuery();

                    return result == 1;
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        private Post GetLikesPostFromReader(SqlDataReader reader)
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
