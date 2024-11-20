using ChatApp.Data.Entities;
using ChatApp.Models;
using ChatApp.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IFileRepository _fileRepository;
        // Dictionary to track online users: Key is user ID, Value is connection ID
        private static readonly Dictionary<string, string> OnlineUsers = new();

        public ChatHub(IMessageRepository messageRepository, IFileRepository fileRepository)
        {
            _messageRepository = messageRepository;
            _fileRepository = fileRepository;
        }

        public async Task SendMessage(string messageText, string recipientId, string senderId, IFormFile attachment = null)
        {
            string attachmentUrl = null;

            // Check if there's an attachment to upload
            if (attachment != null)
            {
                // Upload the attachment and get its URL
                attachmentUrl = await _fileRepository.UploadFile("Attachments", attachment);
            }

            // Create the message object
            var message = new CreateMessage
            {
                Text = messageText,
                SentAt = DateTime.Now,
                RecipientId = recipientId,
                AttachmentUrl = attachment
            };

            // Save the message in the repository
            await _messageRepository.SendMessageAsync(message, senderId);

            // Send message to the recipient via SignalR
            await Clients.User(recipientId).SendAsync("ReceiveMessage", messageText, senderId, attachmentUrl);
        }


        public async Task NotifyMessageRead(string messageId, string recipientId)
        {
            // Verify that the message has been read
            await _messageRepository.MarkMessageAsReadAsync(int.Parse(messageId));
            await Clients.User(recipientId).SendAsync("MessageRead", messageId);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;

            if (!string.IsNullOrEmpty(userId))
            {
                // Add the user to the online list
                OnlineUsers[userId] = Context.ConnectionId;

                // Notify all clients about the updated online users
                await Clients.All.SendAsync("UpdateOnlineUsers", OnlineUsers.Keys);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;

            if (!string.IsNullOrEmpty(userId))
            {
                // Remove the user from the online list
                OnlineUsers.Remove(userId);

                // Notify all clients about the updated online users
                await Clients.All.SendAsync("UpdateOnlineUsers", OnlineUsers.Keys);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public static List<string> GetOnlineUsers()
        {
            lock (OnlineUsers)
            {
                return OnlineUsers.Values.Distinct().ToList();
            }
        }
    }
}
