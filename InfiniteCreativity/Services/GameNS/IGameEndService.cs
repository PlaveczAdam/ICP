namespace InfiniteCreativity.Services.GameNS
{
    public interface IGameEndService
    {
        public Task Endgame(string gConnectionId, bool removeGameObjectsOnly = false);
    }
}
