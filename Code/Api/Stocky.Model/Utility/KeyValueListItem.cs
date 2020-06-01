using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Stocky.Model.Utility
{
    public class KeyValueListItem<T>
    {
        [Description("key")]
        public T Value { get; set; }
        [Description("value")]
        public string Text { get; set; }

        public KeyValueListItem(T value, string text)
        {
            Value = value;
            Text = text;
        }
        public KeyValueListItem()
        {
        }
    }
}
