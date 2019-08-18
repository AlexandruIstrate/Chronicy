using Chronicy.Data;
using Chronicy.Uwp.ViewModels;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Chronicy.Uwp.Views
{
    /// <summary>
    /// Home page for viewing and managing Notebooks, Stacks and Cards.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private Notebook lastSelectedItem;

        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            MasterListView.ItemsSource = LoadItems();

            UpdateForVisualState(AdaptiveStates.CurrentState);

            // Don't play a content transition for first item load.
            // Sometimes, this content will be animated as part of the page transition.
            DisableContentTransitions();
        }

        private void OnAdaptiveStatesChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }

        private void OnListClicked(object sender, ItemClickEventArgs e)
        {
            Notebook clickedItem = (Notebook)e.ClickedItem;
            lastSelectedItem = clickedItem;

            if (AdaptiveStates.CurrentState == NarrowState)
            {
                // Use "drill in" transition for navigating from master list to detail view
                Frame.Navigate(typeof(HomeDetailsPage), clickedItem.ID, new DrillInNavigationTransitionInfo());
            }
            else
            {
                // Play a refresh animation when the user switches detail items.
                EnableContentTransitions();
            }
        }

        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
            bool isNarrow = (newState == NarrowState);

            if (isNarrow && oldState == DefaultState && lastSelectedItem != null)
            {
                // Resize down to the detail item. Don't play a transition.
                Frame.Navigate(typeof(HomeDetailsPage), lastSelectedItem.ID, new SuppressNavigationTransitionInfo());
            }

            EntranceNavigationTransitionInfo.SetIsTargetElement(MasterListView, isNarrow);

            if (DetailContentPresenter != null)
            {
                EntranceNavigationTransitionInfo.SetIsTargetElement(DetailContentPresenter, !isNarrow);
            }
        }

        private void EnableContentTransitions()
        {
            DetailContentPresenter.ContentTransitions.Clear();
            DetailContentPresenter.ContentTransitions.Add(new EntranceThemeTransition());
        }

        private void DisableContentTransitions()
        {
            if (DetailContentPresenter != null)
            {
                DetailContentPresenter.ContentTransitions.Clear();
            }
        }

        private List<Notebook> LoadItems()
        {
            return new List<Notebook>
            {
                new Notebook
                {
                    Name = "Notebook1",
                    Stacks = new List<Stack>
                    {
                        new Stack { Name = "Stack1" }
                    }
                }
            };
        }
    }
}
