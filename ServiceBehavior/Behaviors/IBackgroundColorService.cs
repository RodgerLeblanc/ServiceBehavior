using ServiceBehavior.Abstractions;

namespace ServiceBehavior.Behaviors
{
    public interface IBackgroundColorService : IServiceBehavior
    {
        void SetBackground(string hex);
    }
}