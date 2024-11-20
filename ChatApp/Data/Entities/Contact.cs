using ChatApp.Models;

namespace ChatApp.Data.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string ContactUserId { get; set; }
        public ApplicationUser ContactUser { get; set; }
    }
}
