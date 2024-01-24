using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Authorization;
using WebApi.Dto;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Queue;
using Hangfire;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MailScheduleController : ControllerBase
    {
        MailObj _mailObj = new MailObj();

        [AllowAnonymous]
        [HttpPost]
        [Route("delayed")]
        public IActionResult Delayed(string username)
        {
            var jobId = BackgroundJob.Schedule(() =>  _mailObj.SendDelayedMail(username), TimeSpan.FromMinutes(1));
            return Ok($"JobId: {jobId} completed in 1 minute SendDelayedMail to {username}");
        }
    }
}