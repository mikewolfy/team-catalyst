using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalyst.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalyst.Controllers
{
    [Route("recipients")]
    public class JokeRecipientController : Controller
    {
        private IJokeRecipientData _data;
        public JokeRecipientController(IJokeRecipientData data)
        {
            _data = data;
        }

        // GET: Joke
        public async Task<ActionResult> Index()
        {
            var recipients = await _data.GetAll();
            return View(recipients.OrderBy(r => r.Name).ToList());
        }
    }
}