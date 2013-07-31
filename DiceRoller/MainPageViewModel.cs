using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Phone.Tasks;
using System.Windows;
using Microsoft.Phone.Marketplace;
using System.Windows.Media;

namespace DiceRoller
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            TitleText = "ShadowRoller";
            Dice = new ObservableCollection<Die>();
            DesiredDiceCount = 6;
            DieTypeSource = new DieSelectorSource(6);
        }

        public string TitleText { get; set; }

        public ObservableCollection<Die> Dice { get; set; }

        private int _dicecount = 1;
        public int DesiredDiceCount
        {
            get
            {
                return _dicecount;
            }
            set
            {
                _dicecount = Math.Max(1, value);
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DesiredDiceCount"));
            }
        }

        public int BonusDice { get; set; }

        internal void RequestDiceRoll()
        {
            foreach (var die in Dice)
                if (die.Max != (int)DieTypeSource.SelectedItem)
                    die.Max = (int)DieTypeSource.SelectedItem;
            while (_dicecount > Dice.Count)
                Dice.Add(new Die((int)DieTypeSource.SelectedItem));
            while (_dicecount < Dice.Count)
                Dice.RemoveAt(Dice.Count - 1);
            foreach (var die in Dice)
                die.Roll();
            while (BonusDice > 0 && App.Rules.RuleOfSixesEnabled)
            {
                var d = new Die((int)DieTypeSource.SelectedItem);
                Dice.Add(d);
                d.Roll();
                BonusDice--;
            }
            var badCount = 0;
            var hitCount = 0;
            foreach (var die in Dice)
            {
                if (die.Value >= App.Rules.HitThreshold)
                    hitCount++;
                else if (die.Value == 1)
                    badCount++;
            }

            if (badCount >= Math.Max((Dice.Count / 2.0) - (GremlinsEnabledSwitch ? GremlinsCount : 0), 0))
            {
                if (hitCount == 0)
                    HitStatus = "CRITICAL GLITCH!";
                else
                    HitStatus = string.Format("Glitch, {0} Hits", hitCount);
                HitStatusColor = App.Brushes.GlitchColor;
            }
            else
            {
                HitStatus = string.Format("{0} Hits!", hitCount);
                HitStatusColor = App.Brushes.NormalColor;
            }

            PropertyChanged(this, new PropertyChangedEventArgs("HitStatus"));
            PropertyChanged(this, new PropertyChangedEventArgs("HitStatusColor"));
        }

        public DieSelectorSource DieTypeSource { get; set; }

        public bool ThresholdIs4Switch
        {
            get { return App.Rules.HitThreshold == 4; }
            set { App.Rules.HitThreshold = value ? 4 : 5; }
        }

        public bool RuleOfSixesSwitch
        {
            get { return App.Rules.RuleOfSixesEnabled; }
            set { App.Rules.RuleOfSixesEnabled = value; BonusDice = 0; }
        }

        public bool GremlinsEnabledSwitch
        {
            get { return App.Rules.UseGremlins; }
            set
            {
                App.Rules.UseGremlins = value; PropertyChanged(this, new PropertyChangedEventArgs("GremlinsEnabledSwitch"));
                PropertyChanged(this, new PropertyChangedEventArgs("GremlinsSwitchText"));
            }
        }

        public int GremlinsCount
        {
            get { return App.Rules.GremlinsCount; }
            set { App.Rules.GremlinsCount = value; PropertyChanged(this, new PropertyChangedEventArgs("GremlinsCount")); PropertyChanged(this, new PropertyChangedEventArgs("GremlinsSwitchText")); }
        }

        public string GremlinsSwitchText
        {
            get { return string.Format("Gremlins ({0})", GremlinsEnabledSwitch ? GremlinsCount.ToString() : "Disabled"); }
        }

        public bool ChristmasModeSwitch
        {
            get { return App.Rules.ChristmasMode; }
            set { App.Rules.ChristmasMode = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string HitStatus { get; set; }
        public Brush HitStatusColor { get; set; }

        public Visibility ShowAds
        {
            get
            {
                var li = new LicenseInformation();
                return li.IsTrial() || System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
