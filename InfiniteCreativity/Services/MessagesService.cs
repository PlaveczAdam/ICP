using AutoMapper;
using InfiniteCreativity.Data;
using InfiniteCreativity.Exceptions;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO.Message;
using InfiniteCreativity.Models.Enums;

namespace InfiniteCreativity.Services
{
    public class MessagesService : IMessagesService
    {
        private InfiniteCreativityContext _context;
        private IMapper _mapper;
        private IPlayerService _playerService;
        private INotificationService _notificationService;

        public MessagesService(InfiniteCreativityContext context, IPlayerService playerService, IMapper mapper, INotificationService notificationService)
        {
            _context = context;
            _playerService = playerService;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<ShowMessageDTO>> GetMessages()
        {
            var currentPlayer = await _playerService.GetCurrentPlayer(withMessages:true);
            var messages = new List<Message>();
            messages.AddRange(currentPlayer.SentMessages);
            messages.AddRange(currentPlayer.RecievedMessages);
            var orderedMessages = messages.OrderBy((x) => x.SendDate).DistinctBy((x)=>x.Id);
            return _mapper.Map<IEnumerable<ShowMessageDTO>>(orderedMessages);
        }

        public async Task<ShowMessageDTO> SendMessage(CreateMessageDTO message)
        {
            var recipient = await _playerService.GetPlayerByName(message.RecipientName);
            if (recipient is null)
            {
                throw new RecipientNotFoundException();
            }
            var currentPlayer = await _playerService.GetCurrentPlayer(withMessages: true);
            var msg = new Message();
            msg.SendDate = DateTime.UtcNow;
            msg.Sender = currentPlayer;
            msg.Recipient = recipient;
            msg.MessageBody = message.MessageBody;
            _context.Add(msg);
            await _context.SaveChangesAsync();
            await _notificationService.SendFeNotification(recipient.Id, NotificationType.Message);
            return _mapper.Map<ShowMessageDTO>(msg);
        }
    }
}
