using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace DiceRoller
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            TitleText = "ShadowRoller";
            Dice = new ObservableCollection<Die>();
            DesiredDiceCount = 6;
            SelectedDieType = 6;
            DieTypeSource = new DieSelectorSource(6);
            Options = new ShadowRunRules();//TODO load/save this.
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
            while (_dicecount > Dice.Count)
                Dice.Add(new Die(SelectedDieType));
            while (_dicecount < Dice.Count)
                Dice.RemoveAt(Dice.Count - 1);
            foreach (var die in Dice)
                die.Roll();
            while (BonusDice > 0 && Options.RuleOfSixesEnabled)
            {
                var d = new Die(SelectedDieType);
                Dice.Add(d);
                d.Roll();
                BonusDice--;
            }
            var badCount = 0;
            var hitCount = 0;
            foreach (var die in Dice)
            {
                if (die.Value >= Options.HitThreshold)
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
                //HitStatusColor = App.Brushes.GlitchColor;
            }
            else
            {
                HitStatus = string.Format("{0} Hits!", hitCount);
                //HitStatusColor = App.Brushes.NormalColor;
            }

            PropertyChanged(this, new PropertyChangedEventArgs("HitStatus"));
            PropertyChanged(this, new PropertyChangedEventArgs("HitStatusColor"));
        }

        public DieSelectorSource DieTypeSource { get; set; }

        public bool ThresholdIs4Switch
        {
            get { return Options.HitThreshold == 4; }
            set { Options.HitThreshold = value ? 4 : 5; }
        }

        public bool RuleOfSixesSwitch
        {
            get { return Options.RuleOfSixesEnabled; }
            set { Options.RuleOfSixesEnabled = value; BonusDice = 0; }
        }

        public bool GremlinsEnabledSwitch
        {
            get { return Options.UseGremlins; }
            set
            {
                Options.UseGremlins = value; PropertyChanged(this, new PropertyChangedEventArgs("GremlinsEnabledSwitch"));
                PropertyChanged(this, new PropertyChangedEventArgs("GremlinsSwitchText"));
            }
        }

        public int GremlinsCount
        {
            get { return Options.GremlinsCount; }
            set { Options.GremlinsCount = value; PropertyChanged(this, new PropertyChangedEventArgs("GremlinsCount")); PropertyChanged(this, new PropertyChangedEventArgs("GremlinsSwitchText")); }
        }

        public string GremlinsSwitchText
        {
            get { return string.Format("Gremlins ({0})", GremlinsEnabledSwitch ? GremlinsCount.ToString() : "Disabled"); }
        }

        public bool ChristmasModeSwitch
        {
            get { return Options.ChristmasMode; }
            set { Options.ChristmasMode = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string HitStatus { get; set; }
        public Brush HitStatusColor { get; set; }

        public Visibility ShowAds => Visibility.Collapsed;
        //{
        //    get
        //    {
        //        var li = new LicenseInformation();
        //        return li.IsTrial() || System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Collapsed;
        //    }
        //}

        int _selectedDieSize;
        public int SelectedDieType { get { return _selectedDieSize; } set { _selectedDieSize = value; Dice.Clear(); } }
        public int[] ValidDieTypes { get { return new[] { 2, 4, 6, 8, 10, 20, 100 }; } }

        public ShadowRunRules Options { get; private set; }
    }
}
