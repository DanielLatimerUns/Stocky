using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ReportViewer.MVVM;
using ReportViewer.WebBrowser;
using MVVM_Framework;



namespace ReportViewer.ViewModels
{
    public class ReportsViewerViewModel : Observable
    {
        private WebBrowserNavigation _Navi;
        private Command _Navigate;
        private string ReportBaseURISTring;
        public ObservableCollection<Stocky.Data.skReport> ReportList { get; set; }

      

        public ReportsViewerViewModel()
        {
            MVVM_Framework.ObjectMessenger om = new ObjectMessenger();
            var server = om.FindObject("SSRS") as string;

            ReportBaseURISTring = server?.ToString();

            _Navi = new WebBrowserNavigation(new Uri(ReportBaseURISTring));
            _Navigate = new Command(Navigator);           
            ReportList = new ObservableCollection<Stocky.Data.skReport>();

            FillTreeView();
        }

        public void FillTreeView()
        {
            try
            {
                foreach (var Report in Stocky.Data.skReport.GetReports(ReportBaseURISTring))
                {
                    ReportList.Add(Report);
                }
            }
            catch (Exception e)
            {
                
            }
        }

        public WebBrowserNavigation Navi
        {
            get { return _Navi; }
            set
            {
                _Navi = value;
                NotifyPropertyChanged("Navi");
            }
        }

        public Command Navigate
        {
            get { return _Navigate; }
        }

        public void Navigator(object Paramater)
        {
            switch(Paramater.ToString())
            {
                case "HOME":
                    Navi.GoHome();
                    break;
                case "REFRESH":
                    Navi.RefreshCurrentPage();
                    break;
            }
        }

        


    }
}
