using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IEmailService
    {
       Task BroadcastEmailAsync();

       Task<bool> AddToQueue(EmailBroadcast email);
    }
}
