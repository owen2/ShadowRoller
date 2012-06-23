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
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = App.MainViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.MainViewModel.RequestDiceRoll();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var desiredDice = Math.Round(e.NewValue);
            while (desiredDice < App.MainViewModel.Dice.Count)
                App.MainViewModel.Dice.RemoveAt(0);
            while (desiredDice > App.MainViewModel.Dice.Count)
                App.MainViewModel.Dice.Add(new Die());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var mpdt = new MarketplaceDetailTask();
            mpdt.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "ShadowRoller";
            emailComposeTask.Body = "";
            emailComposeTask.To = "owen@owenjohnson.info";

            emailComposeTask.Show();
        }

        private void BigStepDownButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.MainViewModel.DesiredDiceCount -= 5;
        }

        private void SmallStepDownButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.MainViewModel.DesiredDiceCount -= 1;
        }

        private void SmallStepUpButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.MainViewModel.DesiredDiceCount += 1;
        }

        private void BigStepUpButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.MainViewModel.DesiredDiceCount += 5;
        }

        private void AdControl_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            System.Diagnostics.Debugger.Log(1, "Ads", e.Error.Message);
            ((Microsoft.Advertising.Mobile.UI.AdControl)sender).Visibility = Visibility.Collapsed;
        }
    }
}