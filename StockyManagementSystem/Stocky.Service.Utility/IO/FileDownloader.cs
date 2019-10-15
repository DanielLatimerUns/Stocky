using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Service.Contracts;
using System.IO;

namespace Stocky.Service.Utility.IO
{
    public static class FileDownloader
    {
        public static RemoteFileInfo DownLoadFile(DownloadRequest request)
        {
            RemoteFileInfo returnfile = new RemoteFileInfo();
            try
            {
                string FileDirectory = (String.Format(@"C:\Dev\Uploads\{0}\{1}", request.TypeID, request.ObjectID));

                if (!Directory.Exists(FileDirectory))
                    throw new DirectoryNotFoundException("This object does not have any files.");
                FileInfo file = new FileInfo(String.Format(@"{0}\{1}", FileDirectory, request.FileName));
                if (!file.Exists)
                    throw new FileNotFoundException("File " + request.FileName + " was not found for this object");

                   FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);

                    returnfile.FileName = file.Name;
                    returnfile.Length = file.Length;
                    returnfile.ObjectID = request.ObjectID;
                    returnfile.TypeDirectory = request.TypeID;
                    returnfile.FileByteStream = stream;

                return returnfile;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public static List<string> GetObjectFileNames(string ObjectID,string TypeID)
        {
            List<string> RetunrList = new List<string>();

            string DirectoryTocheck = (String.Format(@"C:\Dev\Uploads\{0}\{1}", TypeID, ObjectID));

            if (!Directory.Exists(DirectoryTocheck))
                throw new DirectoryNotFoundException("This object does not have any files.");
            var files = Directory.GetFiles(DirectoryTocheck);

            foreach(string s in files)
            {
                FileInfo file = new FileInfo(s);
                if (file.Exists)
                    RetunrList.Add(file.Name);
            }

            if (RetunrList.Count > 0)
                return RetunrList;
            else
                throw new Exception("No files for this object can be found");
        }

    }
}
