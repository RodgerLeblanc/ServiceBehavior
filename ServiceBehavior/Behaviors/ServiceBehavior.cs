using ServiceBehavior.Abstractions;
using System.Windows;
using System.Windows.Interactivity;

namespace ServiceBehavior.Behaviors
{
    public abstract class ServiceBehavior<T> : Behavior<T>
        where T : FrameworkElement
    {
        public string Key { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject.IsLoaded)
            {
                RegisterService();
            }
            else
            {
                AssociatedObject.Loaded += OnLoaded;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            RegisterService();

            AssociatedObject.Loaded -= OnLoaded;
        }

        private void RegisterService()
        {
            if (!(AssociatedObject.DataContext is ISupportServices supportServices))
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(Key))
            {
                supportServices.ServiceContainer.RegisterService(Key, this);
            }
            else
            {
                supportServices.ServiceContainer.RegisterService(this);
            }
        }
    }
}
