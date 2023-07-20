namespace InfiniteCreativity.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        public abstract string Code { get; }
    }
}
