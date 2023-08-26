using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Models
{
    [PrimaryKey(nameof(ConnectionID))]
    public class GConnection
    {
        public string ConnectionID { get; set; }
    }
}
