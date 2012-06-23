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
            HitColor = (Brush)App.Current.Resources["PhoneAccentBrush"];
            GlitchColor = new SolidColorBrush(Colors.Red);
            NormalColor = (Brush)App.Current.Resources["PhoneForegroundBrush"];
            ExplodedColor = (Brush)App.Current.Resources["PhoneAccentBrush"];

            if (DateTime.Today.Month == 12 && 26 > DateTime.Today.Day && DateTime.Today.Day > 22)
                HitColor = new SolidColorBrush(Colors.Green);

            if (DateTime.Today.Month == 7 && 6 > DateTime.Today.Day && DateTime.Today.Day > 2)
                HitColor = new SolidColorBrush(Colors.Blue);

            if (DateTime.Today.Month == 11 && DateTime.Today.DayOfWeek == DayOfWeek.Thursday && DateTime.Today.Day > 20)
                HitColor = new SolidColorBrush(Colors.Orange);

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
            }
        }

        public Brush HitColor { get; set; }
        public Brush GlitchColor { get; set; }
        public Brush NormalColor { get; set; }
        public Brush ExplodedColor { get; set; }
    }
}
