using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallController: ControllerBase
    {
        private readonly WallService _wallService;

        public WallController(WallService wallService)
        {
            _wallService = wallService;
        }

        [HttpGet]
        public ActionResult<List<Wall>> Get() =>
            _wallService.Get();

        [HttpGet("{id:length(24)}", Name = "GetWall")]
        public ActionResult<Wall> Get(string id)
        {
            var wall = _wallService.Get(id);

            if (wall == null)
            {
                return NotFound();
            }

            return wall;
        }

        [HttpPost]
        public ActionResult<Wall> Create(Wall wall)
        {
            _wallService.Create(wall);

            return CreatedAtRoute("Getwall", new { id = wall.Id.ToString() }, wall);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Wall wallIn)
        {
            var wall = _wallService.Get(id);

            if (wall == null)
            {
                return NotFound();
            }

            _wallService.Update(id, wallIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var wall = _wallService.Get(id);

            if (wall == null)
            {
                return NotFound();
            }

            _wallService.Remove(wall.Id);

            return NoContent();
        }
    }
}
