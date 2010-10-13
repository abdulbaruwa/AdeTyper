using System.Windows.Controls;
using AdemolaTyper.ViewModels;
using AdemolaTyper.Views;

namespace AdemolaTyper.Control
{
    public class TyperPanel : Canvas
    {
        private double _totalWordwWidth;
        private int _currentRow;
        private static int letterWidth = 6;
       // private static int letterHeight = 12;
        protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
        {
            return base.ArrangeOverride(arrangeSize);
        }

        protected override void OnInitialized(System.EventArgs e)
        {
            base.OnInitialized(e);
        }

        protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {
            base.OnRender(dc);
        }

        protected override void OnRenderSizeChanged(System.Windows.SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
        }

        protected override void OnVisualParentChanged(System.Windows.DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
        }


        protected override void OnVisualChildrenChanged(System.Windows.DependencyObject visualAdded, System.Windows.DependencyObject visualRemoved)
        {
            var contentPresenter = visualAdded as ContentPresenter;
            var panel = visualAdded as UserControl;
            
            if(contentPresenter != null)
            {                
                var word = contentPresenter.DataContext as WordViewModel;
                if(word != null)
                {
                    //SetLeft(contentPresenter, CalculateLeft(contentPresenter.ActualWidth));
                    //_totalWordwWidth += contentPresenter.ActualWidth;
                    //SetLeft(contentPresenter, CalculateLeftFromVm(word));
                    if (_totalWordwWidth + (word.Letters.Count * letterWidth) > ActualWidth)
                    {
                        _totalWordwWidth = 0;
                        _currentRow++;
                    }
                    SetLeft(contentPresenter, _totalWordwWidth);
                    SetTop(contentPresenter, CalculateTop(word));

                    _totalWordwWidth += word.Letters.Count*letterWidth;
                }
            }
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }

        private double CalculateTop(WordViewModel vm)
        {
            double wordHeight = letterWidth * _currentRow;
            //double vertOffset = (ActualHeight - letterWidth) /2;
            return _currentRow*vm.WordHeight;// letterHeight;// + vertOffset;
        }

        private double CalculateLeftFromVm(WordViewModel vm)
        {
            var oldValue = _totalWordwWidth;
            var letters = vm.Letters.Count;
            var wordWith = (letters*letterWidth);
            _totalWordwWidth += wordWith;// +horizontalOffset;
            return oldValue;
        }

        private double CalculateLeft(double wordWith)
        {
            double horizontalOffset = (ActualWidth - _totalWordwWidth)/2;
            return _totalWordwWidth + wordWith;// +horizontalOffset;
        }

    }
}