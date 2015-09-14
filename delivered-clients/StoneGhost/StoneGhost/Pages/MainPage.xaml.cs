using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StoneGhost.Pages
{
    public sealed partial class MainPage : Page
    {
        public static MainPage CurrentPage;

        private void ToggleButton_Click(object sender, RoutedEventArgs e) => Splitter.IsPaneOpen = !Splitter.IsPaneOpen;

        private void PageControl_SelectionChanged(object sender, SelectionChangedEventArgs e) => ScenarioFrame.Navigate(((sender as ListBox)?.SelectedItem as ApplicationModule)?.ClassType ?? typeof(Connection));

        public void DisplayMessage(string message) => StatusBlock.Text += message;

        public List<ApplicationModule> Modules { get; } = new List<ApplicationModule>
        {
            new ApplicationModule { Title = "Tilkobling", ClassType = typeof(Connection) },
            new ApplicationModule { Title = "Visualisering", ClassType = typeof(MapRenderer)}
        };

        public MainPage()
        {
            InitializeComponent();

            CurrentPage = this;

            ModuleControl.ItemsSource = Modules;

            ScenarioFrame.Navigate(typeof(Connection));
        }
    }
}
