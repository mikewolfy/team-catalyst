using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalyst.Models;
using Catalyst.Services;
using Catalyst.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Catalyst.Controllers
{
    [Route("api/jokes")]
    [ApiController]
    public class JokeApiController : ControllerBase
    {
        private IConfiguration _config;
        private IJokeData _data;

        public JokeApiController(IJokeData data, IConfiguration config)
        {
            _config = config;
            _data = data;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Post([FromBody] Joke joke)
        {
            if (AuthorizationUtils.Authorized(Request, _config) == false)
            {
                return Unauthorized();
            }
            if (joke == null || String.IsNullOrEmpty(joke.JokeText))
            {
                return BadRequest();
            }

            var j = await _data.Add(joke);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<Joke>> Get()
        {
            var jokes = await _data.GetAll();
            return jokes.ToList();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (AuthorizationUtils.Authorized(Request, _config) == false)
            {
                return Unauthorized();
            }
            await _data.Delete(id);
            return Ok();
        }
    }
}