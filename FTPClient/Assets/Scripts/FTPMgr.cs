using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class FTPMgr
{
    private static FTPMgr instance = new FTPMgr();
    public static FTPMgr Instance => instance;

    private string FTP_PATH = "ftp://127.0.0.1/";
    // TODO change to real username and password
    private string USER_NAME = "xxx";
    private string PASSWORD = "xxx";

    public async void DownloadFile(string fileName, string remotePath, UnityAction action = null)
    {
        await Task.Run(() => {
            try
            {
                FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + fileName)) as FtpWebRequest;
                req.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
                req.KeepAlive = false;
                req.UseBinary = true;
                req.Method = WebRequestMethods.Ftp.DownloadFile;
                req.Proxy = null;

                Stream downloadStream = req.GetResponse().GetResponseStream();
                using (FileStream fileStream = File.Create(remotePath))
                {
                    byte[] bytes = new byte[1024];
                    int contentLength = downloadStream.Read(bytes, 0, bytes.Length);
                    while (contentLength != 0)
                    {
                        fileStream.Write(bytes,0,contentLength);
                        contentLength = downloadStream.Read(bytes, 0, bytes.Length);
                    }
                    downloadStream.Close();
                    fileStream.Close();
                }
                Debug.Log("DOWNLOAD SUCCESS");
            }
            catch (Exception e)
            {
                Debug.Log("DOWNLOAD FAILED " + e.Message);
                throw;
            }
            action?.Invoke();
        });
    }

    // Multi thread upload to avoid blocking main thread
    public async void UploadFile(string fileName, string localPath, UnityAction action = null)
    {
        await Task.Run(() => {
            try
            {
                FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + fileName)) as FtpWebRequest;
                req.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
                req.KeepAlive = false;
                req.UseBinary = true;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.Proxy = null;

                Stream uploadStream = req.GetRequestStream();
                using (FileStream fileStream = File.OpenRead(localPath))
                {
                    byte[] bytes = new byte[1024];
                    int contentLength = fileStream.Read(bytes, 0, bytes.Length);
                    while (contentLength != 0)
                    {
                        uploadStream.Write(bytes, 0, contentLength);
                        contentLength = fileStream.Read(bytes, 0, bytes.Length);
                    }
                    fileStream.Close();
                    uploadStream.Close();
                }
                Debug.Log("UPLOAD SUCCESS");
            }
            catch (Exception e)
            {
                Debug.Log("UPLOAD FAILED " + e.Message);
                throw;
            }
        });
        action?.Invoke();
    }
}
