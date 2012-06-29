using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media;

namespace DiceRoller
{
    public class Die : INotifyPropertyChanged
    {
        public Die()
        {
            Min = 1;
            Max = 6;
        }

        public Die(int faces)
        {
            Min = 1;
            Max = faces;
        }

        public Die(int min, int max)
        {
            Min = min; Max = max;
        }

        //TODO: Make Constructor for named die;

        private string _type = string.Empty;
        public string Type
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_type))
                    return _type;
                else
                    return "d" + Max;
            }
            set
            { _type = value; }
        }

        public int Min { get; set; }
        public int Max { get; set; }
        private int _value = 0;
        public int Value
        {
            get
            {
                return _value;
            }
        }

        internal void Roll()
        {
            _value = App.Randomizer.Next(Min, Max + 1);
            if (_value == 6)
                App.MainViewModel.BonusDice++;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(""));
            }
        }

        public Brush Color
        {
            get
            {
                if (_value >= App.Rules.HitThreshold && App.Rules.ChristmasMode)
                    return App.Brushes.HitColor;
                else if (_value == Min && App.Rules.ChristmasMode)
                    return App.Brushes.GlitchColor;
                else
                    return App.Brushes.NormalColor;
            }
        }

        public Brush OutlineColor
        {
            get
            {
                if (App.MainViewModel.Dice.IndexOf(this) >= App.MainViewModel.DesiredDiceCount)
                    return App.Brushes.ExplodedColor;
                else
                    return App.Brushes.NormalColor;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
