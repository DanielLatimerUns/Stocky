using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportTools.ViewModels;

using System.Collections.ObjectModel;
using System.Reflection;

namespace Debugger
{
    public class ScreenDebuggerViewModel :ViewModelBase
    {
        private Command _InvokeCommand;
        private Command _GetPropValue;
        ScreenDebugger Debugger;

        public ScreenDebuggerViewModel()
        {
            TreeVIewList = new ObservableCollection<DebugInfo>();
            LoadDebugData();
            _DInfo = ScreenDebugger.CurrentDebugType;
            _InvokeCommand = new Command(invokeMethod);
            _GetPropValue = new Command(GetValue);
            Debugger = new ScreenDebugger();
            
        }

        public ObservableCollection<DebugInfo> TreeVIewList { get; set; }

        private void LoadDebugData()
        {
            foreach(DebugInfo DI in ScreenDebugger.CurrentDebugType.InfoList)
            {
                TreeVIewList.Add(DI);
            }
        }

        private void GetValue()
        {
            Debugger.GetPropValue(SelectedProperty, DInfo);
          
        }

        private void invokeMethod()
        {
           
            Debugger.InvokeMethod(SelectedMethod, DInfo);       
        }

        public Command InvokeCommand
        {
            get { return _InvokeCommand; }
        }

          public Command GetPropValue
        {
            get { return _GetPropValue; }
        }

        private DebugType _DInfo;

        public DebugType DInfo
        {
            get { return _DInfo; }
            set
            {
                _DInfo = value;
                RaisePropertyChanged("DInfo");
            }
        }

        private MethodInfo _SelectedMethod;

        public MethodInfo  SelectedMethod
        {
	        get {return _SelectedMethod; }
            set
            {
               _SelectedMethod = value;
               RaisePropertyChanged("SelectedMethod");
            } 
        }

        private PropertyInfo _SelectedProperty;

        public PropertyInfo SelectedProperty
        {
            get { return _SelectedProperty; }
            set
            {
                _SelectedProperty = value;
                RaisePropertyChanged("SelectedProperty");
            }
        }   
    }
}
