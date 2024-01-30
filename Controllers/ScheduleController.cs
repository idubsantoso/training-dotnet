using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : Controller
    {
        [HttpGet]
        [Route("FireAndForgetJob")]
        public string FireAndForgetJob()
        {
            //Fire - and - Forget Jobs
            //Fire - and - forget jobs are executed only once and almost immediately after creation.
            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Welcome user in Fire and Forget Job Demo!"));

            return $"Job ID: {jobId}. Welcome user in Fire and Forget Job Demo!";
        }

        [HttpGet]
        [Route("DelayedJob")]
        public string DelayedJob()
        {
            //Delayed Jobs
            //Delayed jobs are executed only once too, but not immediately, after a certain time interval.
            var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Welcome user in Delayed Job Demo!"), TimeSpan.FromSeconds(60));

            return $"Job ID: {jobId}. Welcome user in Delayed Job Demo!";
        }

        [HttpGet]
        [Route("ContinuousJob")]
        public string ContinuousJob()
        {
            //Fire - and - Forget Jobs
            //Fire - and - forget jobs are executed only once and almost immediately after creation.
            var parentjobId = BackgroundJob.Enqueue(() => Console.WriteLine("Welcome user in Fire and Forget Job Demo!"));

            //Continuations
            //Continuations are executed when its parent job has been finished.
            BackgroundJob.ContinueJobWith(parentjobId, () => Console.WriteLine("Welcome Sachchi in Continuos Job Demo!"));

            return "Welcome user in Continuos Job Demo!";
        }

        [HttpGet]
        [Route("recurringjob/hours")]
        public string RecurringJobs()
        {
            //Recurring Jobs
            //Recurring jobs fire many times on the specified CRON schedule.
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Welcome user in Recurring Job Demo!"), Cron.Hourly);

            return "Welcome user in Recurring Job Demo!";
        }

    }
}