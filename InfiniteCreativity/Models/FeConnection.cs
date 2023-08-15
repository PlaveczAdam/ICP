using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Models
{
    [PrimaryKey(nameof(ConnectionID))]
    public class FeConnection
    {
        public string ConnectionID { get; set; }
        public string UserAgent { get; set; }
        public bool Connected { get; set; }
    }
}
