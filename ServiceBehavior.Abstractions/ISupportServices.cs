namespace ServiceBehavior.Abstractions
{
    public interface ISupportServices
    {
        IServiceContainer ServiceContainer { get; }
    }
}