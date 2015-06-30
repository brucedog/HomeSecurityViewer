using Caliburn.Micro;

namespace HomeSecurityViewer
{
    public sealed class MainWindowViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;

        public MainWindowViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            DisplayName = "Home Security Viewer";
        }
    }
}
