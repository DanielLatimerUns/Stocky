using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;

namespace Stocky.UI.Dialogs
{
    public class QeustionBox :ViewModelBase
    {
        #region Fields
        #endregion

        #region Properties

        private string _Title;

        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                NotifyPropertyChanged("Title");
            }
        }

        private string _Qustion;

        public string Question
        {
            get { return _Qustion; }
            set
            {
                _Qustion = value;
                NotifyPropertyChanged("Question");
            }
        }

        private string _Answer;

        public string Answer
        {
            get { return _Answer; }
            set { _Answer = value; }
        }

        #endregion

        #region Constuctors
        #endregion

        #region Commands
        #endregion

        #region Command Methods
        #endregion

        #region Static Methods

        public static string Show(string Title,string Question)
        {
            var QB = new Stocky.Views.QuestionBoxView();
            
            var data = (QeustionBox)QB.DataContext;

            data.Question = Question;
            data.Title = Title;

            QB.ShowDialog();

            return data.Answer;
        }

        #endregion

    }
}
