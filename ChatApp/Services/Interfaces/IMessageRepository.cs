using ChatApp.Models;

namespace ChatApp.Services.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<GetAllMessages>> GetChatHistoryAsync(string senderId, string recipientId);
        Task<CreateMessage> SendMessageAsync(CreateMessage message, string UserName);
        Task MarkMessageAsReadAsync(int messageId);
    }
}
