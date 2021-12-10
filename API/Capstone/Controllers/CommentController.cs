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

        [HttpGet("commentList")]
        public ActionResult<List<Comment>> GetCommentsByPost(int postId)
        {
            List<Comment> commentList = commentDao.
        }
    }
}
