namespace InfiniteCreativity.DTO.Message
{
    public class ShowMessageDTO
    {
        public Guid Id { get; set; }
        public ShowPlayerDTO Sender { get; set; }
        public ShowPlayerDTO Recipient { get; set; }
        public string MessageBody { get; set; }
        public DateTime SendDate { get; set; }
        public bool FromInbox { get; set; }
    }
}
