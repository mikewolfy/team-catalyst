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
    [Route("api/joke-recipients")]
    [ApiController]
    public class JokeRecipientApiController : ControllerBase
    {
        private IConfiguration _config;
        private IJokeRecipientData _data;

        public JokeRecipientApiController(IJokeRecipientData data, IConfiguration config)
        {
            _config = config;
            _data = data;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Post([FromBody] JokeRecipient recipient)
        {
            if (AuthorizationUtils.Authorized(Request, _config) == false)
            {
                return Unauthorized();
            }
            if (recipient == null || String.IsNullOrEmpty(recipient.Name) || String.IsNullOrEmpty(recipient.Email))
            {
                return BadRequest();
            }

            var j = await _data.Add(recipient);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JokeRecipient>>> Get()
        {
            if (AuthorizationUtils.Authorized(Request, _config) == false)
            {
                return Unauthorized();
            }

            var recipients = await _data.GetAll();
            return recipients.ToList();
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
