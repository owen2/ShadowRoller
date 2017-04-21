using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace DiceRoller
{
    public class ColorSettings
    {
        public ColorSettings()
        {
            HitColor = (SolidColorBrush)App.Current.Resources["PhoneAccentBrush"];
            GlitchColor = new SolidColorBrush(Colors.Red);
            NormalColor = (SolidColorBrush)App.Current.Resources["PhoneForegroundBrush"];
            ExplodedColor = (SolidColorBrush)App.Current.Resources["PhoneAccentBrush"];

            if (HitColor.Color.ToString() == "#FFE51400")
                HitColor = new SolidColorBrush(Colors.Green);

            if (DateTime.Today.Month == 12 && 26 > DateTime.Today.Day && DateTime.Today.Day > 23)
            {
                HitColor = new SolidColorBrush(Colors.Green);
                ExplodedColor = new SolidColorBrush(Colors.Green);
            }

            if (DateTime.Today.Month == 7 && DateTime.Today.Day == 4)
            {
                HitColor = new SolidColorBrush(Colors.Blue);
                ExplodedColor = new SolidColorBrush(Colors.Red);
            }

            if (DateTime.Today.Month == 11 && DateTime.Today.DayOfWeek == DayOfWeek.Thursday && DateTime.Today.Day > 20)
            {
                HitColor = new SolidColorBrush(Colors.Orange);
                ExplodedColor = new SolidColorBrush(Colors.Red);
                NormalColor = new SolidColorBrush(Colors.Brown);
            }

            if (DateTime.Today.Month == 3 && DateTime.Today.Day == 17)
            {
                HitColor = new SolidColorBrush(Colors.Green);
                NormalColor = HitColor;
                GlitchColor = NormalColor;
                ExplodedColor = GlitchColor;
            }

            if (DateTime.Today.Month == 2 && DateTime.Today.Day == 14)
            {
                HitColor = new SolidColorBrush(Colors.Magenta);
                GlitchColor = new SolidColorBrush(Colors.Red);
                NormalColor = new SolidColorBrush(Colors.Purple);
                ExplodedColor = new SolidColorBrush(Colors.Red);
            }
        }

        public SolidColorBrush HitColor { get; set; }
        public SolidColorBrush GlitchColor { get; set; }
        public SolidColorBrush NormalColor { get; set; }
        public SolidColorBrush ExplodedColor { get; set; }
    }
}
