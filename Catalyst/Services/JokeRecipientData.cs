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
    public class JokeRecipientData : IJokeRecipientData
    {
        private CatalystDbContext _context;

        public JokeRecipientData(CatalystDbContext context)
        {
            _context = context;
        }
        public async Task<JokeRecipient> Add(JokeRecipient jr)
        {
            var existing = _context.JokeRecipients.Where(r => r.Email == jr.Email).DefaultIfEmpty(null).First();
            if (existing == null)
            {
                _context.JokeRecipients.Add(jr);
                await _context.SaveChangesAsync();
            }

            return jr;
        }

        public async Task<JokeRecipient> Get(int id)
        {
            return await _context.JokeRecipients.Where(jr => jr.Id == id).DefaultIfEmpty(null).FirstAsync();
        }

        public async Task<IEnumerable<JokeRecipient>> GetAll()
        {
            return await _context.JokeRecipients.OrderBy(jr => jr.Name).ToListAsync();
        }
        public async Task Delete(int id)
        {
            var jr = _context.JokeRecipients.Where(r => r.Id == id).FirstOrDefault();
            if (jr != null)
            {
                _context.JokeRecipients.Remove(jr);
                await _context.SaveChangesAsync();
            }
        }
    }
}
