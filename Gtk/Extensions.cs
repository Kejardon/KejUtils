using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gtk;

namespace KejUtils
{
    public static partial class Extensions
    {
        public static bool GetPrev(this ListStore model, TreeIter iter, out TreeIter prevIter)
        {
            TreePath path = model.GetPath(iter);
            int[] index = path.Indices;
            if (index[index.Length - 1] > 0)
            {
                index[index.Length - 1]--;
                TreePath prevPath = new TreePath(index);
                model.GetIter(out prevIter, prevPath);
                return true;
            }
            prevIter = default(TreeIter);
            return false;
        }
        public static bool GetNext(this ListStore model, TreeIter iter, out TreeIter nextIter)
        {
            TreePath path = model.GetPath(iter);
            int[] index = path.Indices;
            index[index.Length - 1]++;
            TreePath nextPath = new TreePath(index);
            if (model.GetIter(out nextIter, nextPath))
                return true;
            return false;
        }

        public static Button Make(this Button button, EventHandler clickAction, string buttonName)
        {
            if (button == null)
            {
                if (buttonName != null)
                    button = new Button(buttonName);
                else
                    button = new Button();
            }
            else
            {
                if (buttonName != null)
                    button.Label = buttonName;
            }
            if (clickAction != null)
                button.Clicked += clickAction;

            return button;
        }
    }
}
