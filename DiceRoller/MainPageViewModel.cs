using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Phone.Tasks;
using System.Windows;
using Microsoft.Phone.Marketplace;

namespace DiceRoller
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            TitleText = "ShadowRoller";
            AvailableDiceCounts = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            Dice = new ObservableCollection<Die>();
            DesiredDiceCount = 6;
        }

        public List<int> AvailableDiceCounts { get; set; }

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
                _dicecount = value;
                //RequestDiceRoll();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DesiredDiceCount"));
            }
        }

        internal void RequestDiceRoll()
        {
            Dice.Clear();
            for (int i = 0; i < _dicecount; i++)
            {
                var d = new Die();
                Dice.Add(d);
                d.Roll();
            }
            while (BonusDice > 0 && App.Rules.RuleOfSixesEnabled)
            {
                var d = new Die();
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
                if (hitCount == 0)
                    HitStatus = "CRITICAL GLITCH!";
                else
                    HitStatus = string.Format("Glitch, {0} Hits", hitCount);
            else
                HitStatus = string.Format("{0} Hits!", hitCount);

            PropertyChanged(this, new PropertyChangedEventArgs("HitStatus"));
        }

        public bool ThresholdIs4Switch
        {
            get { return App.Rules.HitThreshold == 4; }
            set { App.Rules.HitThreshold = value ? 4 : 5; }
        }

        public bool RuleOfSixesSwitch
        {
            get { return App.Rules.RuleOfSixesEnabled; }
            set { App.Rules.RuleOfSixesEnabled = value; }
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

        public int BonusDice { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string HitStatus { get; set; }

        public Visibility ShowAds
        {
            get
            {
                var li = new LicenseInformation();
                return li.IsTrial() ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
