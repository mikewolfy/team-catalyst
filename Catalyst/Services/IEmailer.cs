using Catalyst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalyst.Services
{
    public interface IEmailer
    {
        Task EmailJoke(JokeToEmail joke, List<ToEmailAddress> toEmails);
    }
}
