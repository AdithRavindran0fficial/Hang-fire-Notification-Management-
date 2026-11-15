using Microsoft.EntityFrameworkCore;
using NotificationManagement.Entities;
namespace NotificationManagement
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
            
        }

        public DbSet<Notification> Notifications { get; set; }

    }
}
