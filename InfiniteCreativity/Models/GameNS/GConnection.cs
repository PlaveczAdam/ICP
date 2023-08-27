using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Models.GameNS
{
    [PrimaryKey(nameof(ConnectionID))]
    public class GConnection
    {
        public string ConnectionID { get; set; }
        public int Turn { get; set; } = 1;
    }
}
