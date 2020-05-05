using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatePostController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly UserService _userService;

        public CreatePostController(
            PostService postService, 
            UserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        [HttpPost("{userId}")]
        public ActionResult<Post> Create(Post post, string userId)
        {
            var user = _userService.GetForUser(userId);

            user.Posts.Add(post.PostId);

            _userService.Update(user.Id, user);

            _postService.Create(post);

            return post;
        }
    }
}