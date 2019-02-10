using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalyst.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalyst.Controllers
{
    public class JokeController : Controller
    {
        private IJokeData _data;
        public JokeController(IJokeData data)
        {
            _data = data;
        }

        // GET: Joke
        public async Task<ActionResult> Index()
        {
            var jokes = await _data.GetAll();
            return View(jokes.OrderByDescending(j => j.Id).ToList());
        }
    }
}