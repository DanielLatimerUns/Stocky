using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MVVM_Framework;
using System.ComponentModel;

namespace Stocky.Data
{
    public abstract class skTransactionBase : DataModelBase
    {
        #region Fields

        internal ObjectMessenger Messanger = new ObjectMessenger();
        #endregion

        #region Properties

        public int ID { get; set; }

        public TransactionType TransactionType { get; set; }

        public DateTime? TransactionTime { get; set; }

        //public decimal? Amount { get; set; }

        private decimal? _Amount;
            
        public decimal? Amount
        {
            get { return _Amount; }
            set
            {
                _Amount = value;
                RaisePropertyChanged("Amount");
            }
        }

        //public string Title { get; set; }

        private string _Title;

        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                RaisePropertyChanged("Title");
            }
        }

        //public string Description { get; set; }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                RaisePropertyChanged("Description");
            }
        }

        public string SearchString { get { return Title + Description; } }
        #endregion

        #region Constructors
        public skTransactionBase()
        {
        }
        public skTransactionBase(skUser CurrentUserObj)
            :base(CurrentUserObj)
        {
        }
        #endregion

        #region Methods

        #endregion

    }
}
