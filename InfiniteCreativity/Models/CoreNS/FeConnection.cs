using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Models.CoreNS
{
    public class FeConnection
    {
        public Guid Id { get; set; }
        public string ConnectionID { get; set; }
        public string UserAgent { get; set; }
        public bool Connected { get; set; }
    }
}
