using ServiceBehavior.Abstractions;

namespace ServiceBehavior.ViewModels
{
    public abstract class BaseViewModel : ISupportServices
    {
        protected BaseViewModel()
        {
        }

        protected BaseViewModel(IServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        private IServiceContainer _serviceContainer = null;
        IServiceContainer ISupportServices.ServiceContainer
        {
            get
            {
                if (_serviceContainer == null)
                {
                    _serviceContainer = new ServiceContainer();
                }

                return _serviceContainer;
            }
        }

        protected ISupportServices AsInterface { get => this; }

        protected T GetService<T>()
            where T : class
        {
            return AsInterface.ServiceContainer.GetService<T>();
        }

        protected T GetService<T>(string key)
            where T : class
        {
            return AsInterface.ServiceContainer.GetService<T>(key);
        }
    }
}