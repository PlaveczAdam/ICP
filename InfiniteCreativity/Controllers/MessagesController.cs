using InfiniteCreativity.Models.DTO.Message;
using InfiniteCreativity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace InfiniteCreativity.Controllers
{
    [Authorize]
    [ApiController, Route("api/messages")]
    public class MessagesController:ControllerBase
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
        public async Task<ShowMessageDTO> SendMessage([FromBody]CreateMessageDTO message)
        { 
            return await _messagesService.SendMessage(message);
        }
    }
}
