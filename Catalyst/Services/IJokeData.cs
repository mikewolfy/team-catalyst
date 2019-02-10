using Catalyst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalyst.Services
{
    public interface IJokeData
    {
        Task<Joke> Add(Joke Joke);
        Task<Joke> Get(int id);
        Task<IEnumerable<Joke>> GetAll();
        Task Delete(int id);
    }
}
