namespace NotificationManagement.Entities
{
    public class Notification
    {
        public int Id { get; set; }

        public string RecipientEmail { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
