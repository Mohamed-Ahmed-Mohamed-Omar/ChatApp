namespace ChatApp.Models
{
    public class GetAllMessages
    {
        public string Text { get; set; }
        public bool IsRead { get; set; }

        // Attachment file field
        public IFormFile AttachmentUrl { get; set; }

    }
}
