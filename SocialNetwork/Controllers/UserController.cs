using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Services;
using System.Collections.Generic;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SocialNetworkService _socialNetworkService;

        public UserController(SocialNetworkService socialNetworkService)
        {
            _socialNetworkService = socialNetworkService;
        }

        [HttpGet]
        public ActionResult<List<Users>> Get() =>
            _socialNetworkService.Get();

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<Users> Get(string id)
        {
            var user = _socialNetworkService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public ActionResult<Users> Create(Users user)
        {
            _socialNetworkService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Users usersIn)
        {
            var user = _socialNetworkService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _socialNetworkService.Update(id, usersIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _socialNetworkService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _socialNetworkService.Remove(user.Id);

            return NoContent();
        }
    }
}