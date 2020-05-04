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
        private readonly UserService _userService;

        public FeedUserController(FeedService feedService, UserService userService)
        {
            _feedService = feedService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<Feed>> Get() =>
            _feedService.Get();

        [HttpGet("{id:length(24)}", Name = "GetFeedForUser")]
        public ActionResult<List<Feed>> Get(string id)
        {
            var feeds = _feedService.GetForUser(id);

            if (feeds == null)
            {
                return NotFound();
            }

            return feeds;
        }

        //[HttpPost]
        //public ActionResult<Feed> Create(Feed feed)
        //{
        //    _feedService.Create(feed);

        //    return CreatedAtRoute("GetFeedForUser", new { id = feed.Id.ToString() }, feed);
        //}

        //[HttpPut("{id:length(24)}")]
        //public IActionResult Update(string id, Feed feedIn)
        //{
        //    var feed = _feedService.Get(id);

        //    if (feed == null)
        //    {
        //        return NotFound();
        //    }

        //    _feedService.Update(id, feedIn);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public IActionResult Delete(string id)
        //{
        //    var feed = _feedService.Get(id);

        //    if (feed == null)
        //    {
        //        return NotFound();
        //    }

        //    _feedService.Remove(feed.Id);

        //    return NoContent();
        //}
    }
}