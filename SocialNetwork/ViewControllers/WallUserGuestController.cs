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
        private readonly UserService _userService;
        private readonly PostService _postService;
        private readonly BlockedService _blockedService;
        private readonly CircleService _circleService;

        public WallUserGuestController(
            UserService userService,
            PostService postService, 
            BlockedService blockedService,
            CircleService circleService)
        {
            _userService = userService;
            _postService = postService;
            _blockedService = blockedService;
            _circleService = circleService;
        }

        [HttpGet("{userId}/{guestId}")]
        public ActionResult<List<Post>> Get(string userId, string guestId)
        {
            List<Post> wallPosts = new List<Post>();

            //Check if guestId is blocked by userId
            var blockedList = _blockedService.GetForUser(userId);

            foreach (var blockedUserId in blockedList.BlockedUserIds)
            {
                if (blockedUserId == guestId)
                {
                    return wallPosts;
                }
            }

            //Get all post by userId

            var userPostIds = _userService.GetForUser(guestId).Posts;

            foreach (var userPostId in userPostIds)
            {
                var post = _postService.GetForPostId(userPostId);
                var circleIds = post.CircleRef;

                //If post is public, add to the wall
                if (post.IsPublic)
                {
                    wallPosts.Add(post);
                }
                else
                {
                    //Else check if guestId is member in userId's circle 
                    foreach (var circleId in circleIds)
                    {
                        if (_circleService.IsUserInCircle(circleId, guestId))
                        {
                            wallPosts.Add(post);
                        }
                    }
                }
            }
            
            return wallPosts;
        }
    }
}