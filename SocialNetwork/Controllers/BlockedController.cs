using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockedController : ControllerBase
    {
        private readonly BlockedService _blockedService;

        public BlockedController(BlockedService blockedService)
        {
            _blockedService = blockedService;
        }

        [HttpGet]
        public ActionResult<List<Blocked>> Get() =>
            _blockedService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBlockedList")]
        public ActionResult<Blocked> Get(string id)
        {
            var blocked = _blockedService.Get(id);

            if (blocked == null)
            {
                return NotFound();
            }

            return blocked;
        }

        [HttpPost]
        public ActionResult<Blocked> Create(Blocked blocked)
        {
            _blockedService.Create(blocked);

            return CreatedAtRoute("GetBlockedList", new { id = blocked.Id.ToString() }, blocked);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Blocked blockedIn)
        {
            var blocked = _blockedService.Get(id);

            if (blocked == null)
            {
                return NotFound();
            }

            _blockedService.Update(id, blockedIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var blocked = _blockedService.Get(id);

            if (blocked == null)
            {
                return NotFound();
            }

            _blockedService.Remove(blocked.Id);

            return NoContent();
        }
    }
}
