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
        private readonly BlockedService _blockedService;
        private readonly CircleService _circleService;

        public WallUserGuestController(
            WallService wallService, 
            PostService postService, 
            BlockedService blockedService,
            CircleService circleService)
        {
            _wallService = wallService;
            _postService = postService;
            _blockedService = blockedService;
            _circleService = circleService;
        }

        [HttpGet]
        public ActionResult<List<Wall>> Get() =>
            _wallService.Get();

        [HttpGet("{userId}/{guestId}")]
        public ActionResult<List<Post>> Get(string userId, string guestId)
        {
            var blockedList = _blockedService.GetForUser(userId);
            List<Post> wallPosts = new List<Post>();

            foreach (var blockedUserId in blockedList.BlockedUserIds)
            {
                if (blockedUserId == guestId)
                {
                    return wallPosts;
                }
            }
            
            var wall = _wallService.GetForUser(userId);

            var wallPostIds = wall.Posts;

            foreach (var wallPostId in wallPostIds)
            {
                var post = _postService.GetForPostId(wallPostId);
                var circles = post.CircleRef;

                if (post.IsPublic)
                {
                    wallPosts.Add(post);
                }
                else
                {
                    foreach (var circle in circles)
                    {
                        var membersInCircle = _circleService.GetForCircleId(circle).MemberIds;
                        foreach (var member in membersInCircle)
                        {
                            if (member == guestId)
                            {
                                if (!wallPosts.Contains(post))
                                {
                                    wallPosts.Add(post);
                                }
                            }
                        }
                    }
                }
            }
            
            return wallPosts;
        }
    }
}