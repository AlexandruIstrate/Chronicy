using Chronicy.Data;
using Chronicy.Uwp.ViewModels;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Chronicy.Uwp.Views
{
    /// <summary>
    /// Details page for the Home master/detail.
    /// </summary>
    public sealed partial class HomeDetailsPage : Page
    {
        public const int WidthBreakpoint = 720;

        private static readonly DependencyProperty itemProperty =
            DependencyProperty.Register("Item", typeof(Notebook), typeof(HomeDetailsPage), new PropertyMetadata(null));

        public DependencyProperty ItemProperty => itemProperty;

        public Notebook Item
        {
            get => (Notebook)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        public HomeDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            IList<PageStackEntry> backStack = Frame.BackStack;
            int backStackItemCount = backStack.Count;

            // Register for hardware and software back request from the system
            SystemNavigationManager navigationManager = SystemNavigationManager.GetForCurrentView();
            navigationManager.BackRequested += OnBackRequested;
            navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            // Mark event as handled so we don't get bounced out of the app.
            e.Handled = true;

            // Page above us will be our master view.
            // Make sure we are using the "drill out" animation in this transition.
            Frame.GoBack(new DrillInNavigationTransitionInfo());
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {

        }

        private bool ShouldGoToWideState()
        {
            return Window.Current.Bounds.Width > WidthBreakpoint;
        }
    }
}
