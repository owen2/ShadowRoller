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
            var file = IsolatedStorageFile.GetUserStoreForApplication().CreateFile("prefs.xml");
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(ShadowRunRules));
            serializer.Serialize(file, App.Rules);
            file.Flush();
            file.Close();
        }
        public static void LoadSettings()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ShadowRunRules));
            FileStream fs = new FileStream("prefs.xml", FileMode.Open);
            App.Rules  = (ShadowRunRules)serializer.Deserialize(fs);
            fs.Close();
        }
    }
}
