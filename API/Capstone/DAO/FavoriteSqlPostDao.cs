using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAO;

namespace Capstone.DAO
{
    public class FavoriteSqlPostDao : IFavoritePostDao
    {
        private readonly string connectionString;
        private readonly PostSqlDao postDao; //Using this to grab post ids to see if they match with favorite post ids.
        public FavoriteSqlPostDao(string dbConnectionString, PostSqlDao _postDao)
        {
            connectionString = dbConnectionString;
            postDao = _postDao;
        }
        public bool AddFavoritePost(FavoritePost favoritePost)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO favorited_posts VALUES (@post_id, @account_id);", conn);
                    cmd.Parameters.AddWithValue("@post_id", favoritePost.PostId);
                    cmd.Parameters.AddWithValue("@account_id", favoritePost.AccountId);
                    int result = cmd.ExecuteNonQuery();
                    return result == 1;
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<Post> GetListOfFavoritePosts(int accountId)
        {
            List<Post> favoritePosts = new List<Post>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * from favorited_posts WHERE account_id = @account_id", conn);
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int temppostId = GetFavoritePostFromReader(reader);
                        Post temppost = postDao.GetPost(temppostId);
                        favoritePosts.Add(temppost);
                    }
                }
                return favoritePosts;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            
        }
        public Post GetFavoritePost(int accountId)
        {
            Post favoritePost = null;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * from favorited_posts WHERE account_id = @account_id", conn);
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        int temppostId = GetFavoritePostFromReader(reader);
                        favoritePost = postDao.GetPost(temppostId);
                    }
                }
                return favoritePost;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            
        }
        public bool RemoveFavoritePost(FavoritePost favoritePost)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM favorited_posts WHERE post_id = @post_id AND account_id = @account_id;", conn);
                    cmd.Parameters.AddWithValue("@post_id", favoritePost.PostId);
                    cmd.Parameters.AddWithValue("@account_id", favoritePost.AccountId);
                    int result = cmd.ExecuteNonQuery();
                    return result == 1;
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        private int GetFavoritePostFromReader(SqlDataReader reader) //Used to just get post ids.
        {
            int post = Convert.ToInt32(reader["post_id"]);
            return post;
        }
    }
}
