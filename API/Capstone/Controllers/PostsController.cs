using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Capstone.Services;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class PostsController : ControllerBase
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
        [AllowAnonymous]
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
        [HttpPost("/posts")] //Having issues adding a post I could have a bad
                             //JSON body maybe lol.
        public IActionResult UploadPost([FromForm] NewUploadPost newUploadPost)
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
        [HttpPut("/posts/{postId}")]
        public ActionResult<Post> UpdatePost(Post updatedPost, int postId) //Functions
        {
            bool result = true;
            Post existingPost = postDao.GetPost(postId);
            if(existingPost != null)
            {
                updatedPost.AccountId = existingPost.AccountId;
                updatedPost.PostId = existingPost.PostId;

                result = postDao.UpdatePost(postId, updatedPost.MediaLink, updatedPost.Caption);
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
        [HttpDelete("/posts/{postId}/{accountId}")] //Reference Constraint Issues
                                                    //FK comment_to_post DAO Issue
        public ActionResult<Post> RemovePost(RemovePost removedPost)
        {
            bool deletedPost = postDao.RemovePost(removedPost);
            if (deletedPost == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("/posts/{postId}/like")] //Functions
        public ActionResult<List<int>> GetAccountsWhoLikedPost(int postId)
        {
            List<int> idList = likePostDao.GetAccountIdsLikingPost(postId);

            return Ok(idList);
        }

        [HttpPost("/posts/{postId}/like")] //FK Constraint = FK_liked_post_account_id
                                           //I can add Postid = 1 and AccountId = 1 as much as I want
                                           //But when I do Postid = 5 and Account = 1 it crashes.
        public IActionResult LikePost(LikePost likePost)
        {
            bool newLikedPost = likePostDao.LikePost(likePost);
            if (newLikedPost == true)
            {
                return Ok();
            } 
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("/posts/{postId}/like")] //Functions

        public IActionResult RemoveLikedPost(LikePost likePost)
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

        [HttpPost("/posts/favorites")] //Weird bug where you can't add more than one favorite post
                                       //to the same account without having a FK CONSTRAINT.
                                       //But it can be the same post?
        public ActionResult AddFavoritePost(FavoritePost favoritePost)
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

        [HttpDelete("/posts/favorites/{accountId}")] //Functions
        public ActionResult RemoveFavoritePost(FavoritePost favoritePost)
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

    }

}
