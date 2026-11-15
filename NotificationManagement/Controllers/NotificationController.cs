using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationManagement.Entities;

namespace NotificationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        public NotificationController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotificatio([FromBody] Notification notification)
        {
            notification.Status = "Pending";    
            notification.CreatedAt = DateTime.UtcNow;   
             
            _dbContext.Notifications.Add(notification);

            await _dbContext.SaveChangesAsync();

            //enque job 
             BackgroundJob.Enqueue<NotificationJob>(job => job.Execute(notification.Id));
            return Ok(new
            {
                Message = "Notification created and job enqueued",
                NotificationId = notification.Id
            });
        }

        [HttpPost("scheduled")]
        public IActionResult ScheduleJob(int Id)
        {
            var jobId = BackgroundJob.Schedule<NotificationJob>(job => job.Execute(Id), TimeSpan.FromMinutes(2));
            return Ok(new
            {
                Message = "Notification job scheduled",
                JobId = jobId
            });
        }
            
    }
}
