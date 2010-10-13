using System.Windows;
using System.Windows.Controls;

namespace AdemolaTyper.Control
{
    public class WordPanel : StackPanel
    {
        private Size lastRenderedSize;
        private Size arrangedSize;

        protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
        {
            arrangedSize = arrangeSize;
            lastRenderedSize = base.ArrangeOverride(arrangeSize);
            return lastRenderedSize;
        }
    }
}