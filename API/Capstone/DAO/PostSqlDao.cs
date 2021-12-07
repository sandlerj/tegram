using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;

namespace Capstone.DAO
{
    public class PostSqlDao : IPostDao
    {
        private readonly string connectionString;
        public PostSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public Post GetPost(int postId)
        {
            Post returnPost = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT post_id, account_id, media_link, caption, timestamp FROM posts WHERE post_id = @post_id", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnPost = GetPostFromReader(reader);
                    }
                }
                return returnPost;
            }
            catch
            {
                throw;
            }
        }
        

        public void AddFavoritePost(int postId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO favorited_posts VALUES (@post_id);", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public Post GetFavoritePost(int postId)
        {
            Post returnpost = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT post_id, account_id FROM favorited_posts JOIN posts ON posts.post_id = favorited_posts.post_id WHERE post_id = @post_id", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnpost = GetPostFromReader(reader);
                    }
                }
                return returnpost;
            }catch
            {
                throw;
            }
        }
        public void LikePost(int postId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO posts VALUES (@post_id);", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public void RemoveFavoritePost(int postId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM favorited_posts WHERE post_id = @post_id", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }


        public void UnlikePost(int postId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM liked_posts WHERE post_id = @post_id", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public void UploadPost(int postId, string MediaLink)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO posts VALUES (@post_id, @media_link);", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);
                    cmd.Parameters.AddWithValue("@media_link", MediaLink);

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        private Post GetPostFromReader(SqlDataReader reader)
        {
            Post post = new Post()
            {
                PostId = Convert.ToInt32(reader["post_id"]),
                AccountId = Convert.ToInt32(reader["account_id"]),
                MediaLink = Convert.ToString(reader["media_link"]),
                Caption = Convert.ToString(reader["caption"]),
                Timestamp = Convert.ToString(reader["timestamp"])
            };
            return post;
        }

       
    }
}
