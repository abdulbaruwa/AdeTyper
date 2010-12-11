using System;
using System.Diagnostics;
using System.Windows;
using MVVMLib.Helpers;

namespace AdemolaTyper.ViewModels
{
    public class ViewModelProps
    {

        #region DesignTimeViewModelType
        /// <summary>
        /// DesignTime : The type of the ViewModel 
        /// to use (for design time)
        /// </summary>
        public static readonly DependencyProperty
            DesignTimeViewModelTypeProperty =
            DependencyProperty.RegisterAttached(
            "DesignTimeViewModelType",
                typeof(Type), typeof(ViewModelProps),
                    new FrameworkPropertyMetadata((Type)null));

        /// <summary>
        /// Gets the DesignTimeViewModelType property.  
        /// </summary>
        public static Type GetDesignTimeViewModelType(DependencyObject d)
        {
            return (Type)d.GetValue(DesignTimeViewModelTypeProperty);
        }

        /// <summary>
        /// Sets the DesignTimeViewModelType property.  
        /// </summary>
        public static void SetDesignTimeViewModelType(DependencyObject d,
            Type value)
        {
            d.SetValue(DesignTimeViewModelTypeProperty, value);
        }
        #endregion

        #region ViewModelType

        /// <summary>
        /// ViewModelType : The type of the ViewModel 
        /// to use (for non design time)
        /// </summary>
        public static readonly DependencyProperty ViewModelTypeProperty =
            DependencyProperty.RegisterAttached("ViewModelType",
                typeof(Type), typeof(ViewModelProps),
                    new FrameworkPropertyMetadata((Type)null,
                        new PropertyChangedCallback(
                            OnViewModelTypeChanged)));

        /// <summary>
        /// Gets the ViewModelType property.  
        /// </summary>
        public static Type GetViewModelType(DependencyObject d)
        {
            return (Type)d.GetValue(ViewModelTypeProperty);
        }

        /// <summary>
        /// Sets the ViewModelType property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetViewModelType(DependencyObject d,
            Type value)
        {
            d.SetValue(ViewModelTypeProperty, value);
        }

        /// <summary>
        /// Handles changes to the ViewModelType property.
        /// </summary>
        private static void OnViewModelTypeChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            object vm = null;
            if (Designer.IsDesignMode)
            {
                vm = Activator.CreateInstance(GetDesignTimeViewModelType(d));
                Debug.Print("Hello World");
            }
            else
            {
                vm = Activator.CreateInstance(
                    GetViewModelType(d));
            }

            if (vm == null)
                throw new InvalidOperationException(
                    "You have to specify a type for the ViewModel");

            element.DataContext = vm;
        }

        #endregion

    }

}