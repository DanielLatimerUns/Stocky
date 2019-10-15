using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;


namespace Stocky.ExepionLogger
{
    public class Logger
    {
        public static void LogBasicException(Exception exobj)
        {
            if (exobj != null)
            {
                string FileTowrite;
                string FileLocation = @"E:\Stocky\Logs\StockyLog.txt";

                FileTowrite = String.Format
                    (@"{0}-{1}:{2}{0}StackTrace:{3}{0}Further:{4}", Environment.NewLine, DateTime.Now.ToString(), exobj.Message, exobj.StackTrace, exobj.InnerException);

                File.AppendAllText(FileLocation, Environment.NewLine + FileTowrite);
            }
        }

        public static void LogException(Exception e, string CustomErrorMsg = null)
        {
            XDocument xmllog;
            string InnerExptionData = "No Exception Data Found";
            string FileSaveLocation = @"C:\Dev\XML\"
                                        + e.GetType().FullName
                                        + "_"
                                        + DateTime.Now.ToLongTimeString().Replace(":", "_");
            if (e.InnerException != null)
                InnerExptionData = e.InnerException.StackTrace;
            if (CustomErrorMsg == null)
                CustomErrorMsg = "No Message";

            if(!Directory.Exists(FileSaveLocation))
            {
                Directory.CreateDirectory(FileSaveLocation);
            }

            if (e != null)
            {
                xmllog = new XDocument
                    (
                    new XComment("Exception Error Log"),
                    new XElement("Exception",
                        new XElement("Message", CustomErrorMsg),
                        new XElement("Details",
                            new XAttribute("DateTime", DateTime.Now)
                            ),
                        new XElement("Exeption_Details",
                            new XElement("StackTrace", e.StackTrace),
                            new XElement("HResult", e.HResult),
                            new XElement("Source", e.Source),
                            new XElement("Inner_expection",
                                new XAttribute("StackTrace", InnerExptionData),
                                new XAttribute("Type", e.GetType().FullName)
                             )
                            ),
                        new XElement("Enviroment",
                            new XElement("UserName", Environment.UserName),
                            new XElement("Machine", Environment.MachineName)
                            )
                        )
                    );
                if (!File.Exists(FileSaveLocation + ".xml"))
                    xmllog.Save(FileSaveLocation + ".xml");
                else
                    xmllog.Save(FileSaveLocation + "_1.xml");
            }
        }


        public static void Show(Exception exobj)
        {
            ErrorBox eb = new ErrorBox();
            eb.Show(exobj);
        }

    }
}
