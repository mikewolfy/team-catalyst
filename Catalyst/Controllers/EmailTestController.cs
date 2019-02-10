using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalyst.Services;
using Catalyst.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalyst.Controllers
{
    [Route("api/email-test")]
    [ApiController]
    public class EmailTestController : ControllerBase
    {
        private IEmailer _emailer;

        public EmailTestController(IEmailer emailer)
        {
            _emailer = emailer;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Post([FromBody] string email)
        {
            if (AuthorizationUtils.Authorized(Request) == false)
            {
                return Unauthorized();
            }
            if (String.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

            await _emailer.EmailJoke(new JokeToEmail { Text = "Some funny joke.", Punchline = "funny punchline!" }, 
                new List<ToEmailAddress> { new ToEmailAddress { Email = email, Name = "Recipient" } });
            return Ok("joke email sent");
        }

    }
}