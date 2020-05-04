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

        public FeedUserController(FeedService feedService)
        {
            _feedService = feedService;
        }

        [HttpGet]
        public ActionResult<List<Feed>> Get() =>
            _feedService.Get();

        [HttpGet("{userId}")]
        public ActionResult<List<Feed>> Get(string userId)
        {
            var feeds = _feedService.GetForUser(userId);

            if (feeds == null)
            {
                return NotFound();
            }

            return feeds;
        }
    }
}