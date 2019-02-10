using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalyst.Models
{
    public class Joke
    {
        public int Id { get; set; }
        public string JokeText { get; set; }
        public string Punchline { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
