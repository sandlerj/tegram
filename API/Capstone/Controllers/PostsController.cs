using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAO;
using Microsoft.AspNetCore.Mvc;
using Capstone.Services;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class PostsController : Controller
    {
        private readonly IPostDao postDao;
        private readonly IFavoritePostDao favoritePostDao;
        private readonly ILikePostDao likePostDao;
        private IFileStorageService fileStorageService;

        public PostsController(IPostDao _postDao, IFavoritePostDao _favoritePostDao, ILikePostDao _likePostDao)
        {
            postDao = _postDao;
            favoritePostDao = _favoritePostDao;
            likePostDao = _likePostDao;
            fileStorageService = new AWSS3FileStorage();
        }

        [HttpGet("/posts")] //Functions
        public ActionResult<List<Post>> GetPosts()
        {
            return Ok(postDao.GetAllPosts());

        }

        [HttpGet("/posts/{postId}")] //Functions
        public Post GetPost(int postId)
        {
            Post post = postDao.GetPost(postId);
            if (post.PostId != postId)
            {
                return null;
            } 
            else
            {
                return post;
            }
            
        }
        [HttpPost("/posts")] //Works on frontend
        public IActionResult UploadPost([FromForm] NewUploadPost newUploadPost)
        {
            if (isAuthorized(newUploadPost.AccountId))
            {

                //(Post post, IFormFile uploadImg)
                Post post = new Post
                {
                    AccountId = newUploadPost.AccountId,
                    Caption = newUploadPost.Caption,
                    Timestamp = newUploadPost.Timestamp
                };

                string mediaLink = fileStorageService.UploadFileToStorage(newUploadPost.uploadImg);
                post.MediaLink = mediaLink;
                Post createdPost = postDao.UploadPost(post);
                if (createdPost != null)
                {
                     return Created($"/{post.PostId}", createdPost);
                }
                return BadRequest(new { message = "Could not process your post." });

            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpPut("/posts/{postId}")] //Functions
        public ActionResult<Post> UpdatePost(Post updatedPost, int postId) 
        {
            if (isAuthorized(updatedPost.AccountId))
            {
                bool result = true;
                Post existingPost = postDao.GetPost(postId);
                if(existingPost != null)
                {
                    updatedPost.AccountId = existingPost.AccountId;
                    updatedPost.PostId = existingPost.PostId;

                    result = postDao.UpdatePost(updatedPost);
                } 
                else if (existingPost == null)
                {
                    return NotFound();
                }
                if(result)
                {
                    return Ok();
                } 
                else
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpDelete("{postId}")] //Functions
        public ActionResult<Post> RemovePost(int postId)
        {
            Post post = postDao.GetPost(postId);
            if (isAuthorized(post.AccountId))
            {
                bool deletedPost = postDao.RemovePost(postId);
                if (deletedPost == true)
                {
                    return NoContent();
                }
                else
                {
                    return Forbid();
                }
            }
            else
            {
                return Unauthorized();
            }

        }

        [HttpGet("/posts/{postId}/like")] //Functions
        public ActionResult<List<int>> GetAccountsWhoLikedPost(int postId)
        {
            List<int> idList = likePostDao.GetAccountIdsLikingPost(postId);

            return Ok(idList);
        }

        [HttpPost("/posts/{postId}/like")] //Functions
        public IActionResult LikePost(LikePost likePost)
        {
            if (isAuthorized(likePost.AccountId))
            {
                List<int> accountsLikingPost = likePostDao.LikePost(likePost);
                return Ok(accountsLikingPost);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("/posts/{postId}/like")] //Functions

        public IActionResult RemoveLikedPost(LikePost likePost)
        {
            if (isAuthorized(likePost.AccountId))
            {
                bool deletedPost = likePostDao.UnlikePost(likePost);
                if (deletedPost == true)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("/posts/favorites/{accountId}")] //Functions
        public List<Post> GetFavoritePosts(int accountId)
        {
            List<Post> listpost = favoritePostDao.GetListOfFavoritePosts(accountId);
            if (listpost.Count == 0)
            {
                return null;
            } 
            else
            {
                return listpost;
            }
        }

        [HttpPost("/posts/favorites")] //Functions
        public ActionResult AddFavoritePost(FavoritePost favoritePost)
        {
            if (isAuthorized(favoritePost.AccountId))
            {
                bool newFavoritePost = favoritePostDao.AddFavoritePost(favoritePost);
                if (newFavoritePost == true)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("/posts/favorites/{accountId}")] //Functions
        public ActionResult RemoveFavoritePost(FavoritePost favoritePost)
        {
            if (isAuthorized(favoritePost.AccountId))
            {
                bool removedPost = favoritePostDao.RemoveFavoritePost(favoritePost);
                if(removedPost == true)
                {
                    return NoContent();
                } 
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

    }

}
