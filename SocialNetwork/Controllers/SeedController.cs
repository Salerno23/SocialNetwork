using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Data;
using SocialNetwork.Models;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly DataSeeder _dataSeeder;

        public SeedController(DataSeeder dataSeeder)
        {
            _dataSeeder = dataSeeder;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _dataSeeder.SeedAll();

            return NotFound();
        }
    }
}