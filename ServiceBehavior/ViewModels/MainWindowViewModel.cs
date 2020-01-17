namespace ServiceBehavior.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            SubViewModel = new SubViewModel(AsInterface.ServiceContainer);
        }

        public SubViewModel SubViewModel { get; }
    }
}