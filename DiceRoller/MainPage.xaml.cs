using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace DiceRoller
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = App.MainViewModel;
            DesiredDiceChooser.DataSource = new DieSelectorSource();
            DesiredDiceChooser.DataSource.SelectionChanged += DataSource_SelectionChanged;
        }

        //Cuz its not bindable yet
        void DataSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as MainPageViewModel).DesiredDiceCount = (int)e.AddedItems[0];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.MainViewModel.RequestDiceRoll();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new MarketplaceDetailTask().Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new EmailComposeTask { Subject = "Shadow Roller", To = "owenjohnson@outlook.com" }.Show();
        }

        private void AdControl_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            ((Microsoft.Advertising.Mobile.UI.AdControl)sender).Visibility = Visibility.Collapsed;
        }
    }
}