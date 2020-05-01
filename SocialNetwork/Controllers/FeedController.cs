﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController: ControllerBase
    {
        private readonly FeedService _feedService;

        public FeedController(FeedService feedService)
        {
            _feedService = feedService;
        }

        [HttpGet]
        public ActionResult<List<Feed>> Get() =>
            _feedService.Get();

        [HttpGet("{id:length(24)}", Name = "GetFeed")]
        public ActionResult<Feed> Get(string id)
        {
            var feed = _feedService.Get(id);

            if (feed == null)
            {
                return NotFound();
            }

            return feed;
        }

        [HttpPost]
        public ActionResult<Feed> Create(Feed feed)
        {
            _feedService.Create(feed);

            return CreatedAtRoute("GetFeed", new { id = feed.Id.ToString() }, feed);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Feed feedIn)
        {
            var feed = _feedService.Get(id);

            if (feed == null)
            {
                return NotFound();
            }

            _feedService.Update(id, feedIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var feed = _feedService.Get(id);

            if (feed == null)
            {
                return NotFound();
            }

            _feedService.Remove(feed.Id);

            return NoContent();
        }
    }
}
