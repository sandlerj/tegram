using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Capstone.Models;
using Capstone.DAO;


namespace Capstone.Controllers
{
    [Route("/controller")]
    [ApiController]
    //[Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentDao commentDao;

        public CommentController(ICommentDao _commentDao)
        {
            commentDao = _commentDao;
        }

        [HttpGet("/posts/{id}/comment")]
        public ActionResult<List<Comment>> GetCommentsByPost(int id)
        {
            List<Comment> commentList = commentDao.GetCommentsByPost(id);
            if (commentList != null)
            {
                return Ok(commentList);
            }
            return NotFound();
        }
        [HttpPost("/posts/{id}/comment")]

        public ActionResult<Comment> CreateComment(Comment newComment)
        {
            Comment added = commentDao.CreateComment(newComment);
            return newComment;
        }
    }

   
}
