namespace ChatApp.Models
{
    public class CreateMessage
    {
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }

        // Attachment file field
        public IFormFile AttachmentUrl { get; set; }

        public string SenderId { get; set; }

        public string RecipientId { get; set; }
    }
}
