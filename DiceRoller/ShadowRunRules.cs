using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    public class ShadowRunRules
    {
        public ShadowRunRules()
        {
            RuleOfSixesEnabled = true;
            HitThreshold = 5;
        }

        public bool RuleOfSixesEnabled { get; set; }
        public int HitThreshold { get; set; }
        public bool UseGremlins { get; set; }
        public int GremlinsCount { get; set; }
    }
}
