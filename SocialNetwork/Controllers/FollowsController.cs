using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowsController : ControllerBase
    {
        private readonly FollowsService _followsService;

        public FollowsController(FollowsService followsService)
        {
            _followsService = followsService;
        }

        [HttpGet]
        public ActionResult<List<Follows>> Get() =>
            _followsService.Get();

        [HttpGet("{id:length(24)}", Name = "GetFollows")]
        public ActionResult<Follows> Get(string id)
        {
            var follows = _followsService.Get(id);

            if (follows == null)
            {
                return NotFound();
            }

            return follows;
        }

        [HttpPost]
        public ActionResult<Follows> Create(Follows follows)
        {
            _followsService.Create(follows);

            return CreatedAtRoute("GetFollows", new { id = follows.Id.ToString() }, follows);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Follows followsIn)
        {
            var follows = _followsService.Get(id);

            if (follows == null)
            {
                return NotFound();
            }

            _followsService.Update(id, followsIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var follows = _followsService.Get(id);

            if (follows == null)
            {
                return NotFound();
            }

            _followsService.Remove(follows.Id);

            return NoContent();
        }
    }
}