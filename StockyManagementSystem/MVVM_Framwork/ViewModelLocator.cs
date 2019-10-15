using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

namespace MVVM_Framework
{
    public class ViewModelLocator
    {
        public static bool GetMyPrAutoWireViewModeloperty(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelopertyProperty);
        }

        public static void SetMyPrAutoWireViewModeloperty(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelopertyProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyPrAutoWireViewModeloperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoWireViewModelopertyProperty =
         DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false, AutoWireViewModelChanged));

        public static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d)) return;
            var viewType = d.GetType();
            var ViewTypeName = viewType.Name;
            var viewModelTypeName = "SupportTools." + "ViewModels." + ViewTypeName + "Model";
            var viewModelType = Type.GetType(viewModelTypeName);
            var viewModel = Activator.CreateInstance(viewModelType);
            ((FrameworkElement)d).DataContext = viewModel;
        }

    }
}




