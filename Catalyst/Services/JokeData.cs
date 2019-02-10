using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalyst.Data;
using Catalyst.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;

namespace Catalyst.Services
{
    public class JokeData : IJokeData
    {
        private CatalystDbContext _context;
        private IJokeRecipientData _recipientData;
        private IEmailer _emailer;
        //private IQueueClient _queueClient;

        public JokeData(CatalystDbContext context, IEmailer emailer, IJokeRecipientData recipientData)
        {
            _recipientData = recipientData;
            _emailer = emailer;
            //_queueClient = queueClient;
            _context = context;
        }
        public async Task<Joke> Add(Joke joke)
        {
            var existing = _context.Jokes.Where(s => s.JokeText == joke.JokeText).DefaultIfEmpty(null).First();
            if (existing == null)
            {
                //save the joke
                _context.Jokes.Add(joke);
                await _context.SaveChangesAsync();

                //email the joke
                List<ToEmailAddress> to = new List<ToEmailAddress>();
                var toEmails = await _recipientData.GetAll();
                toEmails.ToList().ForEach(t =>
                {
                    to.Add(new ToEmailAddress { Email = t.Email, Name = t.Name });
                });
                await _emailer.EmailJoke(new JokeToEmail { Text = joke.JokeText, Punchline = joke.Punchline }, to);
            }

            return joke;
        }

        public async Task<Joke> Get(int id)
        {
            return await _context.Jokes.Where(q => q.Id == id).DefaultIfEmpty(null).FirstAsync();
        }

        public async Task<IEnumerable<Joke>> GetAll()
        {
            return await _context.Jokes.OrderBy(q => q.JokeText).ToListAsync();
        }
        public async Task Delete(int id)
        {
            var joke = _context.Jokes.Where(q => q.Id == id).FirstOrDefault();
            if (joke != null)
            {
                _context.Jokes.Remove(joke);
                await _context.SaveChangesAsync();
            }
        }

    }
}
