using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Service.Contracts;
using System.IO;

namespace Stocky.Service.Utility.IO
{
    public static class FileUploader
    {
        public static void Upload(RemoteFileInfo request)
        {
            string SaveDirectory = (String.Format(@"C:\Dev\Uploads\{0}\{1}", request.TypeDirectory, request.ObjectID));
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);
            string path = String.Format(@"{0}\{1}", SaveDirectory,request.FileName);    

            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                const int bufferSize = 4096;
                byte[] buffer = new byte[bufferSize];

                int size = 0;
                try
                {
                    while ((size = request.FileByteStream.Read(buffer, 0, bufferSize)) > 0)
                    {
                        stream.Write(buffer, 0, size);
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    stream.Close();
                    request.FileByteStream.Close();
                }
            }
        }
    }
}
