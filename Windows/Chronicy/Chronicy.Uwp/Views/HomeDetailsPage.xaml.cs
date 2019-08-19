using Chronicy.Data;
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

            if (backStackItemCount > 0)
            {
                PageStackEntry masterPageEntry = backStack[backStackItemCount - 1];
                backStack.RemoveAt(backStackItemCount - 1);

                // Doctor the navigation parameter for the master page so it
                // will show the correct item in the side-by-side view.
                PageStackEntry modifiedEntry = new PageStackEntry(
                    masterPageEntry.SourcePageType,
                    Item.ID,
                    masterPageEntry.NavigationTransitionInfo
                    );
                backStack.Add(modifiedEntry);
            }

            // Register for hardware and software back request from the system
            SystemNavigationManager navigationManager = SystemNavigationManager.GetForCurrentView();
            navigationManager.BackRequested += OnBackRequested;
            navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            SystemNavigationManager navigationManager = SystemNavigationManager.GetForCurrentView();
            navigationManager.BackRequested -= OnBackRequested;
            navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            // Mark event as handled so we don't get bounced out of the app.
            e.Handled = true;

            TransitionBack();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (ShouldGoToWideState())
            {
                // We shouldn't see this page since we are in "wide master-detail" mode.
                // Play a transition as we are navigating from a separate page.
                NavigateBackForWideState(useTransition: true);
            }
            else
            {
                // Realize the main page content.
                FindName(RootPanel.Name);
            }

            Window.Current.SizeChanged += OnWindowSizeChanged;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= OnWindowSizeChanged;
        }

        private void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            if (ShouldGoToWideState())
            {
                // Make sure we are no longer listening to window change events.
                Window.Current.SizeChanged -= OnWindowSizeChanged;

                // We shouldn't see this page since we are in "wide master-detail" mode.
                NavigateBackForWideState(useTransition: false);
            }
        }

        private void TransitionBack()
        {
            // Page above us will be our master view.
            // Make sure we are using the "drill out" animation in this transition.

            Frame.GoBack(new DrillInNavigationTransitionInfo());
        }

        private void NavigateBackForWideState(bool useTransition)
        {
            // Evict this page from the cache as we may not need it again.
            NavigationCacheMode = NavigationCacheMode.Disabled;

            if (useTransition)
            {
                Frame.GoBack(new EntranceNavigationTransitionInfo());
            }
            else
            {
                Frame.GoBack(new SuppressNavigationTransitionInfo());
            }
        }

        private bool ShouldGoToWideState()
        {
            return Window.Current.Bounds.Width > WidthBreakpoint;
        }
    }
}
