using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DiceRoller
{
    [XmlRoot]
    public class ShadowRunRules
    {
        public ShadowRunRules()
        {
            HitThreshold = 5;
        }
        [XmlAttribute]
        public bool RuleOfSixesEnabled { get; set; }
        [XmlAttribute]
        public int HitThreshold { get; set; }
        [XmlAttribute]
        public bool UseGremlins { get; set; }
        [XmlAttribute]
        public int GremlinsCount { get; set; }
        [XmlAttribute]
        public bool ChristmasMode { get; set; }
    }
}
