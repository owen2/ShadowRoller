using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Serialization;

namespace DiceRoller
{
    public static class Persister
    {
        public static void SaveSettings()
        {
            IsolatedStorageSettings.ApplicationSettings["prefs"] = App.Rules;
        }
        public static void LoadSettings()
        {
            try
            {
                App.Rules = (ShadowRunRules)IsolatedStorageSettings.ApplicationSettings["prefs"];
            }
            catch (Exception)
            {
                App.Rules = new ShadowRunRules();
            }

        }
    }
}
