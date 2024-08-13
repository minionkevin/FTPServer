using System;
using System.IO;
using System.Net;
using UnityEngine;

public class FTPComponent : MonoBehaviour
{
    void Start()
    {
        try
        {
            FtpWebRequest req = FtpWebRequest.Create(new Uri("ftp://127.0.0.1/pic.png")) as FtpWebRequest;
            req.Proxy = null;
            NetworkCredential n = new NetworkCredential("xxx", "xxx");
            req.Credentials = n;
            req.KeepAlive = false;
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.UseBinary = true;
            
            Stream upLoadStream = req.GetRequestStream();
            using (FileStream file = File.OpenRead(Application.streamingAssetsPath + "/Test.png"))
            {
                byte[] bytes = new byte[1024];
                int contentLength = file.Read(bytes, 0, bytes.Length);
                while (contentLength != 0)
                {
                    upLoadStream.Write(bytes,0,contentLength);
                    contentLength = file.Read(bytes, 0, bytes.Length);
                }
                file.Close();
                upLoadStream.Close();
                print("FINISH UPLOAD");
            }
        }
        catch (Exception e)
        {
            print("UPLOAD FAILED " + e.Message);
        }
        
    }
}
