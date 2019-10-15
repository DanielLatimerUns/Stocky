using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using Stocky.Views;
using Stocky.Data.Interfaces;

namespace Stocky.Commands
{
    public class CommandMethods
    {
        protected void CloseCurrentTabMethod()
        {
            try
            {
                UI.Enviroment.CloseCurrentTab();
            }
            catch (Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        protected void LoadModuleMethod(object ModuleName)
        {
            try
            {
                UI.Enviroment.LoadNewTab(ModuleName.ToString());
            }
            catch (Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        protected void LoadModuleFullClassNameMethod(object ModuleName)
        {
            try
            {
                UI.Enviroment.LoadNewTab(ModuleName.ToString(), true);
            }
            catch (Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        protected void LoadNewWindowMethod(object WindowName)
        {
            try
            {
                UI.Enviroment.LoadWindow(WindowName.ToString());
            }
            catch (Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        protected void ExitApplicationMethod(object ClosePromt)
        {
            try
            {
                UI.Enviroment.ExitApplication(bool.Parse(ClosePromt.ToString()));
            }
            catch (Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        protected void ChangeDatabseMethod()
        {
            try
            {
                UI.Enviroment.ChnageDB();
            }
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        protected void LoadSaleTabMethod(object StockItem)
        {
            try
            {
                decimal Value;
                var Result = decimal.TryParse(Stocky.UI.Dialogs.QeustionBox.Show("New Sale", "Please input the sale value of the initial stock item."), out Value);

                if (Result)
                {
                    var stockItem = (Stocky.Data.skStock)StockItem;
                    stockItem.SaleValue = Value;

                    MVVM_Framework.ObjectMessenger OM = new MVVM_Framework.ObjectMessenger();

                    OM.Send("STOCKOBJ", stockItem);

                    UI.Enviroment.LoadNewTab("NewMultiSaleView|New Sale");
                }
                else
                {
                    throw new Exception("Please try again with a valid decimal value");
                }

            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        protected void LoadStockTransactionLinkMethod(object TransactionObject)
        {
            ObjectMessenger Messanger = new ObjectMessenger();

            if (TransactionObject != null)
            {
                var Transaction = (ITransaction)TransactionObject;

                try
                {
                    Messanger.Send("LEDGEROBJ", Transaction);
                    StockLingView SLV = new StockLingView();
                    SLV.ShowDialog();
                }
                catch (Exception E)
                {
                    ExepionLogger.Logger.LogException(E);
                    ExepionLogger.Logger.Show(E);
                }
            }
        }
    }
}
