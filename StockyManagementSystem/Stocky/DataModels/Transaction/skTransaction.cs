using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using MVVM_Framework;
using System.Reflection;
using Stocky.ExepionLogger;
using Stocky.DataAccesse;
using Stocky.DataModels.Transaction;


namespace Stocky.Data
{
    public class skTransaction : skTransactionBase
    {

        #region Properties
        private int _SearchID;

        public int SearchID
        {
            get { return _SearchID; }
            set { _SearchID = value; }
        }

        private string _SearchTitle;

        public string SearchTitle
        {
            get { return _SearchTitle; }
            set { _SearchTitle = value; }
        }

        private string _SearchDescription;

        public string SearchDescription
        {
            get { return _SearchDescription; }
            set { _SearchDescription = value; }
        }

        private TransactionType _SelectedSearchTransactionType;

        public TransactionType SelectedSearchTransactionType
        {
            get { return _SelectedSearchTransactionType; }
            set { _SelectedSearchTransactionType = value; }
        }

        private ITransaction _SelectedTransaction;
        public ITransaction SelectedTransaction
        {
            get { return _SelectedTransaction; }
            set
            {
                _SelectedTransaction = value;
                RaisePropertyChanged("SelectedTransaction");
            }
        }

        public ObservableCollection<TransactionType> TransactionTypes { get; set; }


        #endregion

        #region Constructors
        public skTransaction()
        {

            TransactionTypes = new ObservableCollection<Data.TransactionType>();

            TransactionTypes.Add(TransactionType.All);
            TransactionTypes.Add(TransactionType.Purchase);
            TransactionTypes.Add(TransactionType.Sale);
            TransactionTypes.Add(TransactionType.Refund);

        }
        #endregion

        #region Methods   

        public List<ITransaction> GetTransactionList()
        {
            return DATA.GetTransactionList(SelectedSearchTransactionType, SearchID,SearchTitle,SearchDescription);
        }
        #endregion

    }

    public enum TransactionType
    {
        Purchase = 1,
        Sale = 2,
        Refund = 3,
        All = 0
    }
}
