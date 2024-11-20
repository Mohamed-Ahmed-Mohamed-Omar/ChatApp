using ChatApp.Models;

namespace ChatApp.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }

        // Attachment file field
        public string AttachmentUrl { get; set; }

        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public string RecipientId { get; set; }
        public ApplicationUser Recipient { get; set; }
    }
}
