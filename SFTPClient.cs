using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365SFTP
{
    public  class SFTPClient
    {
        public static string T24_path = System.Configuration.ConfigurationManager.AppSettings["path"];
        public static string host = System.Configuration.ConfigurationManager.AppSettings["host"];
        public static string port = System.Configuration.ConfigurationManager.AppSettings["port"];
        public static string password = System.Configuration.ConfigurationManager.AppSettings["password"];
        public static string username = System.Configuration.ConfigurationManager.AppSettings["username"];
        // you could pass the host, port, usr, pass, and uploadFile as parameters
        int portInt;
        public static void FileUploadSFTP()
        {
            //  var host = "road365devstorageaccount.blob.core.windows.net";
            // var port = 22;
            // var username = "road365devstorageaccount.ket24";
            // var password = "rd63cZHUO+k3AlqoF/E+AidXkVllavRb";try{
            int portInt;
            Int32.TryParse(port, out portInt);

            DateTime dt = DateTime.Now;
            string mon = String.Concat("0", dt.Month);
            string file = ""+dt.Year+""+mon.Substring(mon.Length-2,2) + "" + dt.Day+ ".csv";

            // path for file you want to upload
            var uploadFile = @"C:\\Users\\mmkaranja\\Downloads\\AJUA.pdf";
            uploadFile = @"D:\\projects\\Faulu\\KEN_FAULU_T24_GL_20221003.csv";
            uploadFile = T24_path + "KEN_FAULU_T24_GL_" + file;

            using (var client = new SftpClient(host, portInt, username, password))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    Debug.WriteLine("I'm connected to the client");

                    using (var fileStream = new FileStream(uploadFile, FileMode.Open))
                    {

                        client.BufferSize = 4 * 1024; // bypass Payload error large files
                        client.UploadFile(fileStream, Path.GetFileName(uploadFile));

                    }
                }
                else
                {
                    Debug.WriteLine("I couldn't connect");
                }
            }
        }
    }
}
