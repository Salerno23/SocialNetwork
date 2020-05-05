using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateCommentController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly CommentService _commentService;

        public CreateCommentController(
            PostService postService,
            CommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        [HttpPost("{postId}")]
        public ActionResult<Comment> Create(Comment comment, string postId)
        {
            var post = _postService.GetForPostId(postId);

            post.CommentRef.Add(comment.CommentId);

            _postService.Update(post.Id, post);

            _commentService.Create(comment);

            return comment;
        }
    }
}