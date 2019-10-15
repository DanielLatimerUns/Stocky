using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;
using System.ComponentModel;

namespace Debugger
{
    public class ScreenDebugger
    {
               
         public ScreenDebugger()
         {
          
         }

         public static string GetDebugObj(DependencyObject obj)
         {
             return (string)obj.GetValue(DebugObj);
         }

         public static void SetDebugObj(DependencyObject obj, string value)
         {
             obj.SetValue(DebugObj, value);
         }

         public static DebugType CurrentDebugType { get; set; }
         public static DependencyObject VM { get; set; }

     
         public static readonly DependencyProperty DebugObj =
             DependencyProperty.RegisterAttached("EnableDebug", typeof(string), typeof(ScreenDebugger), new PropertyMetadata("blank",DebugObjChanged));

         private static void DebugObjChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
         {
             if (DesignerProperties.GetIsInDesignMode(d)) return;
             {
                 if (d != null)
                 {
                     Type viewType = d.GetType();
                     Type viewModelType = Type.GetType(string.Format("Stocky.ViewModels.{0}Model, StockyMain", viewType.Name));
                   
                     if (viewModelType != null)
                     {
                         List<DebugInfo> DList = new List<DebugInfo>();
                         DList.Add(new DebugInfo { Assembley = viewType.Assembly.FullName, ClassName = viewType.Name, NameSpace = viewType.Namespace, Type = viewType.FullName });
                         DList.Add(new DebugInfo { Assembley = viewModelType.Assembly.FullName, ClassName = viewModelType.Name, NameSpace = viewModelType.Namespace, Type = viewModelType.FullName });
                         DebugType DType = new DebugType();
                         DType.InfoList = DList;
                         DType.ViewModelMethods = viewModelType.GetMethods(BindingFlags.NonPublic | BindingFlags.Public
                                                                           | BindingFlags.Instance | BindingFlags.Static);                                                                  

                         DType.ViewModelProperties = viewModelType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public
                                                                                 | BindingFlags.Instance | BindingFlags.Static);
                         ScreenDebugger.CurrentDebugType = DType;
                         ScreenDebugger.VM = d;                              
                     }
                 }
             }
         }
      

        public void InvokeMethod(MethodInfo SelectedMethodOBJ,DebugType DInfo)
        {
            if(SelectedMethodOBJ != null)
            {
                var methodtype = 
                    (dynamic)Activator.CreateInstance(Type.GetType((DInfo.InfoList.Where(x => x.ClassName.Contains("ViewModel")).FirstOrDefault().Type)));

                if (SelectedMethodOBJ.GetParameters().Count() == 0)
                {
                    SelectedMethodOBJ.Invoke(methodtype, null);
                }
                else
                {
                    MessageBox.Show("Only paramatless methods can be invoked via reflection!");
                }
            }
        }

        public void GetPropValue(PropertyInfo SelectedPropObj,DebugType DInfo)
        {
            var Proptype = 
                    (dynamic)Activator.CreateInstance(Type.GetType((DInfo.InfoList.Where(x => x.ClassName.Contains("ViewModel")).FirstOrDefault().Type)));

            var result = SelectedPropObj.GetValue(Proptype, null);
            MessageBox.Show("Current Value is : " + result);
            
        }

    }
}
