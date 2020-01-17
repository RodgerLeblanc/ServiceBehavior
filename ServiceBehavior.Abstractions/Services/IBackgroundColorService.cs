namespace ServiceBehavior.Abstractions
{
    public interface IBackgroundColorService : IServiceBehavior
    {
        void SetBackground(string hex);
    }
}