using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace Stocky.Data
{

    public abstract class Validation : INotifyPropertyChanged
    {
        #region Properties
        private object _ValiationObject;

        internal object ValiationObject
        {
            get
            {
                return _ValiationObject;
            }
            set
            {
                _ValiationObject = value;
                RaisePropertyChanged("IsObjectValid");
            }
        }

        private bool _IsObjectValid;

        public bool IsObjectValid
        {
            get { return _IsObjectValid; }
            set
            {
                _IsObjectValid = value;
                RaisePropertyChanged("IsObjectValid");
            }
        }

       

        public List<PropertyValidation> PropertyList { get; private set; }
        #endregion

        #region Fields

   //     DataAccesse.DataFunctions Data = new DataAccesse.DataFunctions();

        #endregion

        #region Constructors
        public Validation()
        {
            PropertyList = new List<PropertyValidation>();
        }
        #endregion

        #region Methods
        public bool Validate()
        {
            try
            {
                if (ValiationObject != null)
                {
                    Type ObjectType = ValiationObject.GetType();
                    PropertyInfo[] Props = ObjectType.GetProperties();

                    PropertyList.Clear();

                    foreach (PropertyInfo Prop in Props)
                    {
                        if (Attribute.IsDefined(Prop, typeof(RequiredData)))
                        {
                            RequiredData Data = Attribute.GetCustomAttribute(Prop, typeof(RequiredData)) as RequiredData;

                            switch (Prop.PropertyType.Name)
                            {
                                case "String":
                                    if (CheckStringProp(Prop))
                                    {
                                        AddToList(Prop.Name, Prop.PropertyType.Name, true, Prop.GetValue(this));
                                    }
                                    else
                                    {
                                        AddToList(Prop.Name, Prop.PropertyType.Name, false, Prop.GetValue(this));
                                    }
                                    break;
                                case "Decimal":
                                    if (CheckIntProp(Prop))
                                    {
                                        AddToList(Prop.Name, Prop.PropertyType.Name, true, Prop.GetValue(this));
                                    }
                                    else
                                    {
                                        AddToList(Prop.Name, Prop.PropertyType.Name, false, Prop.GetValue(this));
                                    }
                                    break;
                                case "Int32":
                                    if (CheckIntProp(Prop))
                                    {
                                        AddToList(Prop.Name, Prop.PropertyType.Name, true, Prop.GetValue(this));
                                    }
                                    else
                                    {
                                        AddToList(Prop.Name, Prop.PropertyType.Name, false, Prop.GetValue(this));
                                    }
                                    break;
                                case "skvendors":
                                case "skValueBands":
                                case "skCategory":
                                    if (CheckEntityProp(Prop))
                                    {
                                        AddToList(Prop.Name, Prop.PropertyType.Name, true, Prop.GetValue(this));
                                    }
                                    else
                                    {
                                        AddToList(Prop.Name, Prop.PropertyType.Name, false, Prop.GetValue(this));
                                    }
                                    break;
                            }
                        }
                    }
                    if (PropertyList.Count > 0)
                    {
                        if (PropertyList.Exists(x => x.IsValid == false))
                        {
                            IsObjectValid = false;
                        }
                        else
                        {
                            IsObjectValid = true;
                        }
                    }
                    else
                    {
                        IsObjectValid = true;
                    }
                }
                return IsObjectValid;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AddToList(string PropName, string PropType, bool IsValid ,object ObjectValue)
        {

            PropertyList.Add(new PropertyValidation { PropName = PropName, PropType = PropType, IsValid = IsValid , ObjectStringValue = ObjectValue});
        }

        private bool CheckStringProp(PropertyInfo Prop)
        {
            if (Prop == null || Prop.PropertyType != typeof(string))
            {
                return false;
            }

            RequiredData Data = Attribute.GetCustomAttribute(Prop, typeof(RequiredData)) as RequiredData;

            if (Prop.GetValue(this) == null || Prop.GetValue(this).ToString() == "")
            {
                return false;
            }

            return true;

        }
        private bool CheckIntProp(PropertyInfo Prop)
        {
            if (Prop == null )
            {
                return false;
            }

            RequiredData Data = Attribute.GetCustomAttribute(Prop, typeof(RequiredData)) as RequiredData;

            if ((Prop.GetValue(this) == null || Prop.GetValue(this).ToString() == ""))
            {
                return false;
            }

            if (Data.AllowZeros == false)
            {
                if ((Convert.ToInt32(Prop.GetValue(this)) == 0))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckEntityProp(PropertyInfo Prop)
        {
            var Type = Prop.PropertyType;      
            switch (Type.Name)
            {
                case "skvendors":
                   var vendor = Prop.GetValue(this) as skvendors;
                    if(vendor != null)
                    {
                        if (vendor.VendorID != 0)
                            return true;
                    }
                    else { return false; }
                    break;
                case "skCategory":
                    var category = Prop.GetValue(this) as skCategory;
                    if (category != null)
                    {
                        if (category.StockTypeID != 0)
                            return true;
                    }
                    else { return false; }
                    break;
                case "skValueBands":
                    var ValueBand = Prop.GetValue(this) as skValueBands;
                    if (ValueBand != null)
                    {
                        if (ValueBand.ID != 0)
                            return true;
                    }
                    else { return false; }
                    break;
            }
            return false;
        }

        internal bool IsDigitsOnly(string StringToCheck)
        {
            foreach (char c in StringToCheck)
            {
                if (c < '0' || c > '9' ||c == '.')
                    return false;
            }
            return true;
        }

        public bool IsFieldEmpty(object Value, string FieldName = null)
        {
            if (Value == null || !string.IsNullOrEmpty(Value.ToString()))
            {
                return true;
            }
            return false;
        }

        internal bool IsDirtyObject()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();

            bool Result;

            try
            {
                DataClient.Open();
                Result = DataClient.IsRecordDirty(ValiationObject);
                return Result;
            }
            finally
            {
                DataClient.Close();
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
    #endregion

    public class RequiredData : Attribute
    {
        public bool AllowZeros { get; set; }
    }

    public class PropertyValidation
    {
        public string PropName { get; set; }
        public string PropType { get; set; }
        public bool IsValid { get; set; }
        public object ObjectStringValue { get; set; }
    }


}


