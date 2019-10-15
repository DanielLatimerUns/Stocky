using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Service.Contracts;
using System.IO;

namespace Stocky.Application
{
    public static class Caching
    {
        #region ObjectCacheFunctions
        public static void CatchObjectFiles(string ObjectID, string ObjecType)
        {
            string SaveDirectory = (String.Format(@"C:\stocky\cache\{0}\{1}", ObjecType, ObjectID));

            Stocky.Proxies.AppClient client = new Proxies.AppClient();

            var filenames = new List<string>();

            foreach(string s in client.GetObjectFileNames(ObjectID, ObjecType))
            {
                if (!File.Exists($@"{SaveDirectory}\{s}"))
                    filenames.Add(s);
            }

            foreach (string filename in filenames)
            {
                DownloadRequest request = new DownloadRequest();
                request.FileName = filename;
                request.ObjectID = ObjectID;
                request.TypeID = ObjecType;

                RemoteFileInfo result = new RemoteFileInfo();
                result = client.DownloadFile(request);
         
                if (!Directory.Exists(SaveDirectory))
                    Directory.CreateDirectory(SaveDirectory);

                string path = String.Format(@"{0}\{1}", SaveDirectory, filename);

                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    const int bufferSize = 4096;
                    byte[] buffer = new byte[bufferSize];

                    int size = 0;
                    try
                    {
                        while ((size = result.FileByteStream.Read(buffer, 0, bufferSize)) > 0)
                        {
                            stream.Write(buffer, 0, size);
                        }
                    }
                    finally
                    {
                        stream.Close();
                        result.FileByteStream.Close();
                    }
                }
            }
        }
        /// <summary>
        /// Deltes any object cached file is it isnt currently in use/locked.
        /// </summary>
        public static void DeleteObjectCache()
        {
            string CacheLocation = @"C:\stocky\cache";

            if (!Directory.Exists(CacheLocation))
                throw new Exception("Chach folder does not exist or is not accesable.");

            foreach(string d in Directory.GetDirectories(CacheLocation))
            {
                foreach (string od in Directory.GetDirectories(d))
                {
                    foreach (string s in Directory.GetFiles(od))
                    {
                        FileInfo file = new FileInfo(s);

                        if (!Caching.IsFileLocked(file))
                            try
                            {
                                file.Delete();
                            }
                            catch (IOException)
                            {
                                throw;
                            }
                    }
                }
            }              
        }

        public static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch(IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return false;
        }



        #endregion
    }
}
