using ServiceBehavior.Abstractions;
using System.Windows.Controls;
using System.Windows.Media;

namespace ServiceBehavior.Behaviors
{
    public class BackgroundColorBehavior : ServiceBehavior<Grid>, IBackgroundColorService
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            //Connect to events
        }

        protected override void OnDetaching()
        {
            //Disconnect from events

            base.OnDetaching();
        }

        public void SetBackground(string hex)
        {
            if (!hex.StartsWith("#"))
            {
                hex = $"#{hex}";
            }

            AssociatedObject.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(hex);
        }
    }
}
