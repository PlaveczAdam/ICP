using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace InfiniteCreativity.Models.CoreNS
{
    public class Message
    {
        public int Id { get; set; }
        public Player Sender { get; set; }
        public Player Recipient { get; set; }
        public string MessageBody { get; set; }
        public DateTime SendDate { get; set; }
        [NotMapped]
        public bool FromInbox { get; set; }
    }
}
