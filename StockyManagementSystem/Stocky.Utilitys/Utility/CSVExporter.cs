using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Stocky.Utility
{
    public class CSVExporter
    {
        public List<dynamic> List { get; set; }
        public string FileName { get; set; }

        public CSVExporter()
        {
            List = new List<dynamic>();
        }

        public void Export()
        {
            if (List == null || List.Count == 0) return;

            //get type from 0th member
            Type t = List[0].GetType();
            string newLine = Environment.NewLine;

            string filename = FileName
                             + t.Name
                             + ".csv";

            if (!File.Exists(filename))
            {
                using (var sw = new StreamWriter(filename))
                {
                    //make a new instance of the class name we figured out to get its props
                    object o = Activator.CreateInstance(t);
                    //gets all properties
                    PropertyInfo[] props = o.GetType().GetProperties();

                    //foreach of the properties in class above, write out properties
                    //this is the header row
                    foreach (PropertyInfo pi in props)
                    {
                        sw.Write(pi.Name.ToUpper() + ",");
                    }
                    sw.Write(newLine);

                    //this acts as datarow
                    foreach (var item in List)
                    {
                        //this acts as datacolumn
                        foreach (PropertyInfo pi in props)
                        {
                            //this is the row+col intersection (the value)
                            string whatToWrite = "" + ',';
                            // Check if the value is null and if so write a blank string.
                            string checkvalue = Convert.ToString(item.GetType().GetProperty(pi.Name).GetValue(item, null));

                            if(checkvalue != null)
                            {
                                whatToWrite = Convert.ToString(item.GetType().GetProperty(pi.Name).GetValue(item, null)).Replace(',', ' ') + ',';
                            }                      

                            sw.Write(whatToWrite);
                        }
                        sw.Write(newLine);
                    }
                }
                if (File.Exists(filename))
                {
                    System.Windows.MessageBoxResult Result = System.Windows.MessageBox.Show
                        (string.Format("File sucsessfully created, Would you like to open the file now?{1}{1}File created: {0} ", filename, Environment.NewLine)
                        , "Export Complete", System.Windows.MessageBoxButton.YesNo);

                    if (Result == System.Windows.MessageBoxResult.Yes)
                    {
                        if (File.Exists(filename))
                            System.Diagnostics.Process.Start(filename);
                    }
                }
            }
        }
    }
}
