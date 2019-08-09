using Chronicy.Uwp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Chronicy.Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Dictionary<string, Type> pages;

        public MainPage()
        {
            InitializePages();
            InitializeComponent();
        }

        private void InitializePages()
        {
            pages = new Dictionary<string, Type>
            {
                [Pages.Home] = typeof(HomePage),
                [Pages.Actions] = typeof(ActionsPage),
                [Pages.History] = typeof(HistoryPage),
                [Pages.Settings] = typeof(SettingsPage)
            };
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems.First((item) => (item as NavigationViewItem).Name == Pages.Home);
            ContentFrame.Navigate(pages[Pages.Home]);
        }

        private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
                return;
            }

            ContentFrame.Navigate(pages[(string)args.InvokedItem]);
        }
    }
}
