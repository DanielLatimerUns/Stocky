using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using Stocky.Session;
using Stocky.Service.Contracts;
using System.Web;
using static Stocky.Application.Caching;

namespace Stocky.Application
{
    public class ImageImporter
    {

        private string SaveLocation = @"C:\stocky\cache";

        public ImageImporter()
        {

        }
        /// <summary>
        /// Opens a Open file dialog and then copies all the selcted files Async to the file store.
        /// </summary>
        /// <param name="TypeDirectory"></param>
        /// <param name="ObjectID"></param>
        /// <returns></returns>
        public async Task SaveImagesAsync(string TypeDirectory,string ObjectID)
        {
            try
            {
                OpenFileDialog File = new OpenFileDialog();
                File.Title = "Select Image to Import";
                File.ValidateNames = true;
                File.Multiselect = true;
                File.Filter = "Image Files (*.png;*.bmp;*.gif;*.jpeg;*.jpg;*.raw)|*.png;*.bmp;*.gif;*.jpeg;*.jpg;*.raw";
                File.ShowDialog();

                if (File.FileNames.Count() != 0)
                {
                    var task = Task.Run(() => CopyFiles(File.FileNames, TypeDirectory, ObjectID));
                    await task;
                }
            }
            catch(Exception)
            {
                throw;
            }                       
        }
        /// <summary>
        /// Copies all the files within the ImageSource array to the file store.
        /// </summary>
        /// <param name="TypeDirectory"></param>
        /// <param name="ObjectID"></param>
        /// <param name="ImagesSource"></param>
        /// <returns></returns>
        public async Task SaveImagesAsync(string TypeDirectory, string ObjectID,string[] ImagesSource)
        {
            try
            {
                var task = Task.Run(() => CopyFiles(ImagesSource, TypeDirectory, ObjectID));
                await task;
            }
            catch(Exception)
            {
                throw;
            }  

        }

        public async Task<IEnumerable<string>> GetImagesAsync(string ObjectID, string ObjecType)
        {
            try
            {
                var task = Task.Run(() => GetLinks(ObjectID, ObjecType));

                return await task;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CopyFiles(string[] SelectedFiles,string TypeDirectory,string ObjectID)
        {
            List<RemoteFileInfo> FilesToUpload = new List<RemoteFileInfo>();
            Proxies.AppClient clientUpload = new Proxies.AppClient();
            try
            {
                foreach (string S in SelectedFiles)
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(S);
                    RemoteFileInfo uploadRequestInfo = new RemoteFileInfo();

                    using (StreamReader stream = new StreamReader(S, false))
                    {
                        uploadRequestInfo.FileName = fileInfo.Name;
                        uploadRequestInfo.Length = fileInfo.Length;
                        uploadRequestInfo.FileByteStream = stream.BaseStream;
                        uploadRequestInfo.ObjectID = ObjectID;
                        uploadRequestInfo.TypeDirectory = TypeDirectory;

                        clientUpload.UploadFiles(uploadRequestInfo);

                        stream.Close();
                    }
                }
            }          
            finally
            {
                clientUpload.Close();
            }
        
        }  

        private IEnumerable<string> GetLinks(string ObjectID, string ObjecType)
        {

                CatchObjectFiles( ObjectID, ObjecType);

                string DirectoryToCheck = string.Format(@"{0}\{1}\{2}", SaveLocation, ObjecType, ObjectID);

                List<string> Links = new List<string>();

                if (Directory.Exists(DirectoryToCheck))
                {
                    foreach (string s in Directory.GetFiles(DirectoryToCheck))
                    {
                        Links.Add(s);
                    }

                    foreach (string img in Links.ToList())
                    {
                        var imgext = Path.GetExtension(img);
                        switch (imgext.ToUpper())
                        {
                            case ".PNG":
                            case ".JEPG":
                            case ".GIF":
                            case ".JPG":
                            case ".RAW":
                                break;
                            default:
                                Links.Remove(img);
                                break;
                        }
                    }
                    return Links;
                }
                else { Directory.CreateDirectory(DirectoryToCheck); return Links; }            
        }
      
    }

    public class ImageObject
    {

    }
}
