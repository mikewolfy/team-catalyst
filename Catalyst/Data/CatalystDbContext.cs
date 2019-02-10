using Catalyst.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalyst.Data
{
    public class CatalystDbContext : DbContext
    {

        public CatalystDbContext(DbContextOptions<CatalystDbContext> options)
            : base(options)
        {

        }
        public DbSet<Joke> Jokes { get; set; }

        public DbSet<JokeRecipient> JokeRecipients { get; set; }
    }
}
