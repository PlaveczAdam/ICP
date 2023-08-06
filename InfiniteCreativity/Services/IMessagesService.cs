using InfiniteCreativity.Models.DTO.Message;

namespace InfiniteCreativity.Services
{
    public interface IMessagesService
    {
        Task<IEnumerable<ShowMessageDTO>> GetMessages();
        Task<ShowMessageDTO> SendMessage(CreateMessageDTO message);
    }
}
