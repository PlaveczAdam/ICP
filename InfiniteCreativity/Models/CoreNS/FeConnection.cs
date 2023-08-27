using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Models.CoreNS
{
    [PrimaryKey(nameof(ConnectionID))]
    public class FeConnection
    {
        public string ConnectionID { get; set; }
        public string UserAgent { get; set; }
        public bool Connected { get; set; }
    }
}
