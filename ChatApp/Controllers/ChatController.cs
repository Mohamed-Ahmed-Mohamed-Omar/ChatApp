using ChatApp.Models;
using ChatApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(IMessageRepository messageRepository, UserManager<ApplicationUser> userManager)
        {
            _messageRepository = messageRepository;
            _userManager = userManager;
        }

        [HttpGet("history/{recipientId}")]
        public async Task<IActionResult> GetChatHistory(string recipientId)
        {
            // Get the current user's ID
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(senderId))
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            // Retrieve chat history between the current user and the recipient
            var messages = await _messageRepository.GetChatHistoryAsync(senderId, recipientId);

            return Ok(messages);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] CreateMessage message)
        {
            // Get the current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            // Save the message
            var sentMessage = await _messageRepository.SendMessageAsync(message, userId);

            return Ok(new
            {
                Message = "Message sent successfully.",
                SentMessage = sentMessage
            });
        }

        [HttpPut("read/{messageId}")]
        public async Task<IActionResult> MarkMessageAsRead(int messageId)
        {
            await _messageRepository.MarkMessageAsReadAsync(messageId);
            return NoContent();
        }
    }
}
