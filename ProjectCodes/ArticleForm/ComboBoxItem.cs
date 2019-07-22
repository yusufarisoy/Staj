using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleForm
{
    public class ComboBoxItem
    {
        public string Text { get;  set; }
        public int ID { get;  set; }
        public int ArticleCount { get;  set; }

        public ComboBoxItem()
        {

        }

        public ComboBoxItem(string text, int id, int count)
        {
            this.Text = text;
            this.ID = id;
            this.ArticleCount = count;
        }

        public override string ToString()
        {
            return $"{this.Text}  ({this.ArticleCount})";
        }


    }
}
