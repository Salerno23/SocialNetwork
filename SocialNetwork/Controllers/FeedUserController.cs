using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedUserController : ControllerBase
    {
        private readonly FeedService _feedService;
        private readonly PostService _postService;
        private readonly FollowsService _followsService;
        private readonly UserService _userService;
        private readonly CircleService _circleService;

        public FeedUserController(
            FeedService feedService, 
            PostService postService,
            FollowsService followsService,
            UserService userService,
            CircleService circleService)
        {
            _feedService = feedService;
            _postService = postService;
            _followsService = followsService;
            _userService = userService;
            _circleService = circleService;
        }

        [HttpGet]
        public ActionResult<List<Feed>> Get() =>
            _feedService.Get();

        [HttpGet("{userId}")]
        public ActionResult<List<Post>> Get(string userId)
        {
            List<Post> feedPosts = new List<Post>();

            //Posts from users userId follows
            var followList = _followsService.GetForUser(userId).FollowedUserIds;

            foreach (var user in followList)
            {
                var userPosts = _userService.GetForUser(user).Posts;
                foreach (var userPostId in userPosts)
                {
                    var post = _postService.GetForPostId(userPostId);

                    if (post.IsPublic)
                    {
                        feedPosts.Add(post);
                    }
                }
            }

            //userId own posts
            var userPostIds = _userService.GetForUser(userId).Posts;

            foreach (var userPostId in userPostIds)
            {
                feedPosts.Add(_postService.GetForPostId(userPostId));
            }


            //TODO - Post to circles I'm member of
            

            var feed = _feedService.GetForUser(userId);

            var feedPostIds = feed.Posts;

            

            foreach (var feedPostId in feedPostIds)
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