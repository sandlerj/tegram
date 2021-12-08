using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public class LikeSqlPostDao
    {
        private readonly string connectionString;
        public LikeSqlPostDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public Post GetLikedPost(int postId, int accountId)
        {
            Post returnLikedpost = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT post_id, account_id FROM liked_posts JOIN posts ON posts.post_id = liked_posts.post_id WHERE post_id = @post_id AND account_id = @account_id", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnLikedpost = GetLikesPostFromReader(reader);
                    }
                }
                return returnLikedpost;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

             
        public void LikePost(int postId, int accountId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO liked_posts VALUES (@post_id, @account_id);", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UnlikePost(int postId, int accountId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM liked_posts WHERE post_id = @post_id AND account_Id = @account_id", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        private Post GetLikesPostFromReader(SqlDataReader reader) //Just in case we have to get the posts the client liked.
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
