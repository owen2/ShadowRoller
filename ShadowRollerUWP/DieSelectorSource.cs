﻿using Microsoft.Phone.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    public class DieSelectorSource : ILoopingSelectorDataSource, INotifyPropertyChanged
    {
        public DieSelectorSource(int initial = 6)
        {
            _selected = initial;
        }

        public object GetNext(object relativeTo)
        {
            return (int)relativeTo + 1;
        }

        public object GetPrevious(object relativeTo)
        {
            return Math.Abs((int)relativeTo - 1);

        }

        private object _selected;
        public object SelectedItem
        {
            get
            {
                return _selected;
            }
            set
            {
                var old = _selected;
                _selected = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
                if (SelectionChanged != null)
                    SelectionChanged(this, new System.Windows.Controls.SelectionChangedEventArgs(new List<object>() { old }, new List<object>() { value }));
            }
        }

        public event EventHandler<System.Windows.Controls.SelectionChangedEventArgs> SelectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
