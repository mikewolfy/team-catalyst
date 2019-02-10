using Catalyst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalyst.Services
{
    public interface IJokeRecipientData
    { 
        Task<JokeRecipient> Add(JokeRecipient JokeRecipient);
        Task<JokeRecipient> Get(int id);
        Task<IEnumerable<JokeRecipient>> GetAll();
        Task Delete(int id);
    }
}
