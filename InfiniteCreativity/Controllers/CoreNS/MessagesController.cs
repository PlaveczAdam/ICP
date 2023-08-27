using InfiniteCreativity.DTO.Message;
using InfiniteCreativity.Services.CoreNS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfiniteCreativity.Controllers.CoreNS
{
    [Authorize]
    [ApiController, Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private IMessagesService _messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        [HttpGet]
        public async Task<IEnumerable<ShowMessageDTO>> GetMessages()
        {
            return await _messagesService.GetMessages();
        }

        [HttpPost]
        public async Task<ShowMessageDTO> SendMessage([FromBody] CreateMessageDTO message)
        {
            return await _messagesService.SendMessage(message);
        }
    }
}
