using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;


namespace SupportTools.RegEditor
{
    /// <summary>
    /// Tool used to easlisy Manipulate the Registry
    /// </summary>
    public class EditorTool
    {
        //    /// <summary>
        //    /// Sets or gets the subkey string.
        //    /// </summary>
        //     public string Subkey {  get; set; }
        //    /// <summary>
        //    /// Value used to set Key Values.
        //    /// </summary>
        //     public string Value { private get; set; }
        //    /// <summary>
        //    /// The key Value within the key you would like to set.
        //    /// </summary>
        //     public string KeyValue { get; set;}
        //     private RegistryKey key;
        //     private string MDBLocation { get; set; }


        //    public string GetMDBLocation()
        //    {
        //        string subkey =@"Software\FWBS\OMS\2.0\Data";
        //        key = Registry.CurrentUser.OpenSubKey(subkey);

        //        if(key != null)
        //        {
        //           MDBLocation =  key.GetValue("MultiDBLocation") as string;
        //        }
        //        return MDBLocation;
        //     }

        //    /// <summary>
        //    /// Sets a single value for any key within the LocalMachine tree.
        //    /// Required Paramaters: Subkey,Value,KeyValue
        //    /// </summary>
        //    public void SetMachineKey()
        //     {
        //         try
        //         {
        //             if (Subkey != null)
        //             {
        //                 key = Registry.LocalMachine.OpenSubKey(Subkey, true);

        //                 if (key != null)
        //                 {
        //                     key.SetValue(KeyValue, Value);
        //                     System.Windows.MessageBox.Show(string.Format("The key Value '{0}' within the key '{1}' has been set to '{2}",KeyValue,Subkey,Value.ToString()), "Registry Updated");
        //                 }
        //                 else
        //                 {
        //                     System.Windows.MessageBox.Show(string.Format("The Key '{0}' or Value '{1}' was not found or is inaccessible.", Subkey, KeyValue), "Registry Not Updated");
        //                 }
        //             }
        //         }
        //         catch (Exception E)
        //         {
        //             System.Windows.MessageBox.Show(E.StackTrace.ToString());
        //         }
        //     }

        //    /// <summary>
        //    /// Sets a single value for any key within the User tree.
        //    /// Required Paramaters: Subkey,Value,KeyValue
        //    /// </summary>
        //    public void SetUserKey()
        //     {

        //        try
        //        {
        //            if (Subkey != null)
        //            {
        //                key = Registry.CurrentUser.OpenSubKey(Subkey, true);

        //                if (key != null)
        //                {
        //                    key.SetValue(KeyValue, Value);
        //                    System.Windows.MessageBox.Show(string.Format("The key Value '{0}' within the key '{1}' has been set to '{2}", KeyValue, Subkey, Value.ToString()), "Registry Updated");
        //                }
        //                else
        //                {
        //                    System.Windows.MessageBox.Show(string.Format("The Key '{0}' or Value '{1}' was not found or is inaccessible.", Subkey, KeyValue), "Registry Not Updated");
        //                }
        //            }
        //        }
        //        catch (Exception E)
        //        {
        //            System.Windows.MessageBox.Show(E.StackTrace.ToString());
        //        }
        //    }
        //    /// <summary>
        //    /// Create a new Machine key with key values 
        //    /// </summary>
        //    /// <param name="Values"></param>
        //     public void CreateMachineKeyWithValues(List<RegistryValues> Values)
        //     {
        //        try
        //        {
        //            key = Registry.LocalMachine.CreateSubKey(Subkey);

        //            if (key != null)
        //            {
        //                foreach (RegistryValues Value in Values)
        //                {
        //                    key.SetValue(Value.ValueName, Value.ValueValue, Value.ValueType);
        //                }
        //                System.Windows.MessageBox.Show(string.Format("The Key '{0}' has been created with the specified Values", Subkey));
        //            }
        //            else
        //            {
        //                System.Windows.MessageBox.Show(string.Format("The Key {0} was not created", key), "Error!");
        //            }
        //        }
        //        catch (Exception E)
        //        {
        //            System.Windows.MessageBox.Show(E.ToString());
        //        }
        //     }
        //    /// <summary>
        //    /// Create a new User Key with Key values
        //    /// </summary>
        //    /// <param name="Values"></param>
        //    public void CreateUserKeyWithValues(List<RegistryValues> Values)
        //    {
        //        try
        //        {
        //            key = Registry.CurrentUser.CreateSubKey(Subkey);

        //            if (key != null)
        //            {
        //                foreach (RegistryValues Value in Values)
        //                {
        //                    key.SetValue(Value.ValueName, Value.ValueValue, Value.ValueType);
        //                }
        //                System.Windows.MessageBox.Show(string.Format("The Key '{0}' has been created with the specified Values", Subkey));
        //            }
        //            else
        //            {
        //                System.Windows.MessageBox.Show(string.Format("The Key {0} was not created", key), "Error!");
        //            }
        //        }
        //        catch (Exception E)
        //        {
        //            System.Windows.MessageBox.Show(E.ToString());
        //        }
        //    }
        //    /// <summary>
        //    /// Create a new user Key Value
        //    /// </summary>
        //    /// <param name="Values"></param>
        //    public void CreateUserValue(List<RegistryValues> Values)
        //    {
        //        key = Registry.CurrentUser.OpenSubKey(Subkey,true);

        //        if (key != null)
        //        {
        //            foreach (RegistryValues Value in Values)
        //            {
        //                key.SetValue(Value.ValueName, Value.ValueValue, Value.ValueType);
        //            }
        //        }
        //        else
        //        {
        //            System.Windows.MessageBox.Show(string.Format("The Key {0} was not created", key), "Error!");
        //        }
        //    }
        //}
    }
}