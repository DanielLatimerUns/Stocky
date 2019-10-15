using System;
using System.Windows;
using ReportViewer.ViewModels;
using Stocky.Data;

namespace ReportViewer.WebBrowser
{
   
    public class WebBrowserNavigation: Observable
    {
        private Command _SetDock;

        public WebBrowserNavigation(Uri StartPage)
        {
            Dock = "Left";           
            _SetDock = new Command(SetListDock);
            BrowserURI = StartPage;
            
        }

        private Uri _BrowserURI;

        public Uri BrowserURI
        {
            get { return _BrowserURI; }
            set
            {
                _BrowserURI = value;
                NotifyPropertyChanged("BrowserURI");
            }
        }

        private ReportLink _SelectedReport;

        public ReportLink SelectedReport
        {
            get { return _SelectedReport; }
            set
            {
                _SelectedReport = value;
                if (_SelectedReport != null)
                BrowserURI = SelectedReport.ReportLocationURI;
                NotifyPropertyChanged("SelectedReport");
            }
        }

        private string _Dock;

        public string Dock
        {
            get { return _Dock; }
            set
            {
                _Dock = value;
                NotifyPropertyChanged("Dock");
            }
        }

        public Command SetDock { get { return _SetDock; } }

        public Uri HomeURI { get; set; }
           

    public static readonly DependencyProperty LinkSourceProperty =
    DependencyProperty.RegisterAttached("LinkSource", typeof(string), typeof(WebBrowserNavigation), new UIPropertyMetadata(null, LinkSourcePropertyChanged));

    public static string GetLinkSource(DependencyObject obj)
    {
        return (string)obj.GetValue(LinkSourceProperty);
    }

    public static void SetLinkSource(DependencyObject obj, string value)
    {
        obj.SetValue(LinkSourceProperty, value);
    }

    // When link changed navigate to site.
    public static void LinkSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        var browser = o as System.Windows.Controls.WebBrowser;
        if (browser != null)
        {
            string uri = e.NewValue as string;
            browser.Source = uri != null ? new Uri(uri) : null;
        }
    }


    public void GoHome()
    {
        if(BrowserURI != HomeURI)
        {
            BrowserURI = HomeURI;
        }
    }

    public void RefreshCurrentPage()
    {
        BrowserURI = new Uri(BrowserURI.ToString());
    }

    public void SetListDock(object Position)
    {
        switch(Position.ToString())
        {
            case "Top":
                Dock = "Top";
                break;
            case "Bottom":
                Dock = "Bottom";
                break;
            case "Right":
                Dock = "Right";
                break;
            case "Left":
                Dock = "Left";
                break;
        }
    }

    public enum Docking
    {
        Left,
        Right,
        Bottom,
        Top
    }    
    }
}
