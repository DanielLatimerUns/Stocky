using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;

namespace Stocky.Commands
{
    public class UIControlCommands : CommandMethods
    {
        private Command _CloseCurrentTab;
        private Command _LoadNewModule;
        private Command _LoadNewModuleFullClassName;
        private Command _LoadNewWindow;
        private Command _ExitApplication;
        private Command _SelectDatabase;
        private Command _LoadSaleTab;
        public Command _LoadStockTransactionLink;

        public UIControlCommands()
        {
            LoadMethods();
        }

        public Command CloseCurrentTab { get { return _CloseCurrentTab; } }
        public Command LoadNewModule { get { return _LoadNewModule; } }
        public Command LoadNewModuleFullClassName { get { return _LoadNewModuleFullClassName; } }
        public Command LoadNewWindow { get { return _LoadNewWindow; } }
        public Command ExitApplication { get { return _ExitApplication; } }
        public Command SelectDatabase { get { return _SelectDatabase; } }
        public Command LoadSaleTab { get { return _LoadSaleTab; } }
        public Command LoadStockTransactionLink { get { return _LoadStockTransactionLink; } }

        protected void LoadMethods()
        {
            _CloseCurrentTab = new Command(CloseCurrentTabMethod);
            _LoadNewModule = new Command(LoadModuleMethod);
            _LoadNewModuleFullClassName = new Command(LoadModuleFullClassNameMethod);
            _LoadNewWindow = new Command(LoadNewWindowMethod);
            _ExitApplication = new Command(ExitApplicationMethod);
            _SelectDatabase = new Command(ChangeDatabseMethod);
            _LoadSaleTab = new Command(LoadSaleTabMethod);
            _LoadStockTransactionLink = new Command(LoadStockTransactionLinkMethod);
        }
    }
}
