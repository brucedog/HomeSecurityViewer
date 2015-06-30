using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Screens;

namespace HomeSecurityViewer
{
    public sealed class MainWindowViewModel : Screen
    {
        private readonly IWindowManager windowManager;

        public MainWindowViewModel(IWindowManager windowManager)
        {
            this.windowManager = windowManager;
            DisplayName = "Home Security Viewer";
        }
    }
}
