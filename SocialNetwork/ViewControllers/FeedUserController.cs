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
        private readonly PostService _postService;
        private readonly FollowsService _followsService;
        private readonly UserService _userService;
        private readonly CircleService _circleService;

        public FeedUserController(
            PostService postService,
            FollowsService followsService,
            UserService userService,
            CircleService circleService)
        {
            _postService = postService;
            _followsService = followsService;
            _userService = userService;
            _circleService = circleService;
        }

        [HttpGet("{userId}")]
        public ActionResult<List<Post>> Get(string userId)
        {
            List<Post> feedPosts = new List<Post>();

            //userId own posts
            var userPostIds = _userService.GetForUser(userId).Posts;

            foreach (var userPostId in userPostIds)
            {
                feedPosts.Add(_postService.GetForPostId(userPostId));
            }

            //Posts to circles I'm Member of
            var circles = _circleService.GetCirclesForUser(userId);

            foreach (var circle in circles)
            {
                var posts =_postService.GetPostsForCircle(circle.CircleId);
                foreach (var post in posts)
                {
                    if (!feedPosts.Contains(post))
                    {
                        feedPosts.Add(post);
                    }
                }
            }

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

            return feedPosts;
        }
    }
}