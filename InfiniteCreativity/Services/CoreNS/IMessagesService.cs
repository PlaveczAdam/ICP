using InfiniteCreativity.DTO.Message;

namespace InfiniteCreativity.Services.CoreNS
{
    public interface IMessagesService
    {
        Task<IEnumerable<ShowMessageDTO>> GetMessages();
        Task<ShowMessageDTO> SendMessage(CreateMessageDTO message);
    }
}
