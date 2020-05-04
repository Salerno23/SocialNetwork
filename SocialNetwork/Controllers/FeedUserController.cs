﻿using System.Collections.Generic;
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

        public FeedUserController(FeedService feedService, PostService postService)
        {
            _feedService = feedService;
            _postService = postService;
        }

        [HttpGet]
        public ActionResult<List<Feed>> Get() =>
            _feedService.Get();

        [HttpGet("{userId}")]
        public ActionResult<List<Post>> Get(string userId)
        {
            var feed = _feedService.GetForUser(userId);

            var feedPostIds = feed.Posts;

            List<Post> feedPosts = new List<Post>();

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