using System;
using System.Diagnostics;
using System.Windows;

namespace MVVMLib.MVVMHelpers
{
    public class ViewModelLoader
    {
        //Dependency property that is attached to a VM Factory
        public static readonly DependencyProperty FactoryTypeProperty =
            DependencyProperty.RegisterAttached("FactoryType", typeof (Type), typeof (ViewModelLoader),
                                                new FrameworkPropertyMetadata((Type) null,
                                                                              new PropertyChangedCallback(
                                                                                  OnFactoryTypeChanged)));

        private static void OnFactoryTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement) d;
            IFactory factory = Activator.CreateInstance(GetFactoryType(d)) as IFactory;
            if (factory == null)
                throw new InvalidOperationException("You have to specify a type that inherits from IFactory");
            var datacontexxt = factory.CreateViewModel(d); 
            if(datacontexxt != null) element.DataContext = datacontexxt;
        }

        private static Type GetFactoryType(DependencyObject dependencyObject)
        {
            return (Type) dependencyObject.GetValue(FactoryTypeProperty);
        }

        public static void SetFactoryType(DependencyObject d, Type value)
        {
            d.SetValue(FactoryTypeProperty, value);
        }
    }
}