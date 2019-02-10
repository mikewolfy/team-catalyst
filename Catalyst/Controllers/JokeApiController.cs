using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalyst.Models;
using Catalyst.Services;
using Catalyst.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalyst.Controllers
{
    [Route("api/jokes")]
    [ApiController]
    public class JokeApiController : ControllerBase
    {
        private IJokeData _data;

        public JokeApiController(IJokeData data)
        {
            _data = data;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Post([FromBody] Joke joke)
        {
            if (AuthorizationUtils.Authorized(Request) == false)
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
        public ActionResult Delete(int id)
        {
            if (AuthorizationUtils.Authorized(Request) == false)
            {
                return Unauthorized();
            }
            _data.Delete(id);
            return Ok();
        }
    }
}