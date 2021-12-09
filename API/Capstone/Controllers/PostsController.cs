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
    [Route("api/[controller]")]
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

        [HttpGet("/posts/{postId}")] 
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
        [HttpPost("/posts")]
        public IActionResult UploadPost(Post post, IFormFile uploadImg)
        {
            IActionResult result = BadRequest(new { message = "Could not process your post." });
            string mediaLink = fileStorageService.UploadFileToStorage(uploadImg);
            post.MediaLink = mediaLink;
            Post createdPost = postDao.UploadPost(post);
            if (createdPost != null)
            {
                result = Created($"/{post.PostId}", createdPost);
            }
            return result;
        }
        [HttpPut("/posts/{postId}")]
        public ActionResult<Post> UpdatePost(Post updatedPost, int postId)
        {
            bool result = true;
            Post existingPost = postDao.GetPost(postId);
            if(existingPost != null)
            {
                updatedPost.AccountId = existingPost.AccountId;
                updatedPost.PostId = existingPost.PostId;

                result = postDao.UpdatePost(updatedPost, postId);
            }
            if(result)
            {
                return Ok();
            } else
            {
                return StatusCode(500);
            }
        }

        [HttpPost("/posts/{postId}/like")] //WORK IN PROGRESS
        public IActionResult LikePost(int postId, int accountId)
        {
            IActionResult result = BadRequest(new { message = "Could not like this post." });
            bool newLikedPost = likePostDao.LikePost(postId, accountId);
            if (newLikedPost == true)
            {
                return Ok();
            } 
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("/posts/{postId}/like")]

        public ActionResult RemoveLikedPost(int postId, int accountId)
        {
            bool result = likePostDao.UnlikePost(postId, accountId);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("/posts/favorites/{accountId}")]

        public List<Post> GetFavoritePost(int postId, int accountId)
        {
            List<Post> listpost = favoritePostDao.GetListOfFavoritePosts(postId, accountId);
            if (listpost.Count == 0)
            {
                return null;
            } 
            else
            {
                return listpost;
            }
        }

        [HttpPost("/posts/favorites")] //Work in progess.
        public ActionResult AddFavoritePost(int postId, int accoundId)
        {
            ActionResult result = BadRequest(new { message = "Could not add favorite post." });
            bool newFavoritePost = favoritePostDao.AddFavoritePost(postId, accoundId);
            if (newFavoritePost == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("/posts/favorites/{accountId}")]
        public ActionResult RemoveFavoritePost(int postId, int accountId)
        {
            Post post = favoritePostDao.GetFavoritePost(postId, accountId);
            if (post == null)
            {
                return NotFound("Could not find the favorited post.");
            }
            bool result = favoritePostDao.RemoveFavoritePost(postId, accountId);
            if(result)
            {
                return NoContent();
            } else
            {
                return BadRequest();
            }
        }

    }

}
