using ChatApp.Data;
using ChatApp.Data.Entities;
using ChatApp.Models;
using ChatApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Services.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileRepository _fileRepository;

        public MessageRepository(ApplicationDbContext context, IFileRepository fileRepository)
        {
            _context = context;
            _fileRepository = fileRepository;
        }

        public async Task<IEnumerable<GetAllMessages>> GetChatHistoryAsync(string senderId, string recipientId)
        {
            var data = await _context.Messages
                .Where(m => (m.SenderId == senderId && m.RecipientId == recipientId) ||
                            (m.SenderId == recipientId && m.RecipientId == senderId))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            return (IEnumerable<GetAllMessages>)data;
        }

        public async Task MarkMessageAsReadAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CreateMessage> SendMessageAsync(CreateMessage message, string UserName)
        {
            Message data = new()
            {
                IsRead = false,
                RecipientId = message.RecipientId,
                SenderId = UserName,
                SentAt = message.SentAt == default ? DateTime.UtcNow : message.SentAt, // التحقق من تاريخ الإرسال
                Text = message.Text,
                AttachmentUrl = await _fileRepository.UploadFile("Images", message.AttachmentUrl)
            };

            await _context.Messages.AddAsync(data);
            await _context.SaveChangesAsync();

            return message;
        }
    }
}
