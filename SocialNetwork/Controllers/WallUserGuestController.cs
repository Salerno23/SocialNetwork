using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallUserGuestController : ControllerBase
    {
        private readonly WallService _wallService;
        private readonly PostService _postService;

        public WallUserGuestController(WallService wallService, PostService postService)
        {
            _wallService = wallService;
            _postService = postService;
        }

        [HttpGet]
        public ActionResult<List<Wall>> Get() =>
            _wallService.Get();

        [HttpGet("{userId}/{guestId}")]
        public ActionResult<List<Post>> Get(string userId, string guestId)
        {
            //TODO
            
            var wall = _wallService.GetForUser(userId);

            var wallPostIds = wall.Posts;

            List<Post> feedPosts = new List<Post>();

            foreach (var feedPostId in wallPostIds)
            {
                feedPosts.Add(_postService.GetForPostId(feedPostId));
            }

            if (feedPosts == null)
            {
                return NotFound();
            }

            return feedPosts;
        }
    }
}