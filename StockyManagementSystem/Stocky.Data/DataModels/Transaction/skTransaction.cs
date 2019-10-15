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
using Stocky.Data.Interfaces;


namespace Stocky.Data
{
    public class skTransaction : skTransactionBase
    {
        #region Fields
 
        #endregion

        #region Properties


        public TransactionType SelectedSearchTransactionType
        {          
            set { SearchFilter.ExtraSearchObject1 = value; }
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

            SelectedSearchTransactionType = TransactionType.All;

        }
        #endregion

        #region Methods   

        public List<ITransaction> GetTransactionList()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                List<ITransaction> returnlist = DataClient.GetTransactionList(this.SearchFilter).TransactionList;
                return returnlist;
            }
            finally
            {
                DataClient.Close();
            }


        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is skRefund))
                return false;
            return ((skTransaction)obj).ID == 0 ? ((skTransaction)obj) == this : ((skTransaction)obj).ID == this.ID;
        }

        public override int GetHashCode()
        {
            return this.ID == 0 ? base.GetHashCode() : this.ID.GetHashCode();
        }

        public override string ToString()
        {
            return Title + Environment.NewLine + Description;
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
