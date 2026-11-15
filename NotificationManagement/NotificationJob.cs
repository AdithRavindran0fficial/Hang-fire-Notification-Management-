using Microsoft.EntityFrameworkCore;
using NotificationManagement.Entities;

namespace NotificationManagement
{
    public class NotificationJob
    {
        private readonly AppDBContext _dbContext;
        private readonly ILogger<NotificationJob> _logger;
        public NotificationJob(AppDBContext appDBContext, ILogger<NotificationJob> logger)
        {
            _dbContext = appDBContext;
            _logger = logger;   
        }

        public async Task Execute(int NotificationId)
        {
            var notification = await _dbContext.Notifications.FirstOrDefaultAsync(n => n.Id == NotificationId);  
            if(notification == null)
            {
                _logger.LogError($"Notification with ID {NotificationId} not found.");
                return;
            }

            _logger.LogInformation(
         "Sending notification to {Email}",
         notification.RecipientEmail);

            //simulate sending email 
            await Task.Delay(1000); // Simulate a delay for sending email   

            notification.Status = "Sent";   
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation(
            "Notification {Id} sent successfully",
            NotificationId);

        }

    }
}
