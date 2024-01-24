using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class MailObj
    {
        public void SendDelayedMail(string username)
        {
            Console.WriteLine($"SendDelayedMail to {username}");
        }
    }
}