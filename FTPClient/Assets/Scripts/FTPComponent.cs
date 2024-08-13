using UnityEngine;

public class FTPComponent : MonoBehaviour
{
    void Start()
    {
        #region Simple upload
        // try
        // {
        //     FtpWebRequest req = FtpWebRequest.Create(new Uri("ftp://127.0.0.1/pic.png")) as FtpWebRequest;
        //     req.Proxy = null;
        //     // TODO change to real username and password
        //     NetworkCredential n = new NetworkCredential("xxx", "xxx");
        //     req.Credentials = n;
        //     req.KeepAlive = false;
        //     req.Method = WebRequestMethods.Ftp.UploadFile;
        //     req.UseBinary = true;
        //     
        //     Stream upLoadStream = req.GetRequestStream();
        //     using (FileStream file = File.OpenRead(Application.streamingAssetsPath + "/Test.png"))
        //     {
        //         byte[] bytes = new byte[1024];
        //         int contentLength = file.Read(bytes, 0, bytes.Length);
        //         while (contentLength != 0)
        //         {
        //             upLoadStream.Write(bytes,0,contentLength);
        //             contentLength = file.Read(bytes, 0, bytes.Length);
        //         }
        //         file.Close();
        //         upLoadStream.Close();
        //         print("FINISH UPLOAD");
        //     }
        // }
        // catch (Exception e)
        // {
        //     print("UPLOAD FAILED " + e.Message);
        // }
        #endregion
        #region Simple download 
        // try
        // {
        //     FtpWebRequest req = FtpWebRequest.Create(new Uri("Ftp://127.0.0.1/pic.png")) as FtpWebRequest;
        //     // TODO change to real username and password
        //     req.Credentials = new NetworkCredential("xxx", "xxx");
        //     req.KeepAlive = false;
        //     req.Method = WebRequestMethods.Ftp.DownloadFile;
        //     req.UseBinary = true;
        //     req.Proxy = null;
        //
        //     FtpWebResponse res = req.GetResponse() as FtpWebResponse;
        //     Stream downloadStream = res.GetResponseStream();
        //
        //     print(Application.persistentDataPath);
        //     using (FileStream fileStream = File.Create(Application.persistentDataPath + "/DownloadPic.png"))
        //     {
        //         byte[] bytes = new byte[1024];
        //         int contentLength = downloadStream.Read(bytes, 0, bytes.Length);
        //         while (contentLength != 0)
        //         {
        //             fileStream.Write(bytes,0,contentLength);
        //             contentLength = downloadStream.Read(bytes, 0, bytes.Length);
        //         }
        //         downloadStream.Close();
        //         fileStream.Close();
        //     }
        //     print("DOWNLOAD SUCCESS");
        // }
        // catch (Exception e)
        // {
        //     print("DOWNLOAD FAILED " + e.Message);
        //     throw;
        // }
        #endregion
        
        FTPMgr.Instance.UploadFile("NewPic.png",Application.streamingAssetsPath + "/Test.png", () => {
                print("Finish uploading");
        });
        
        FTPMgr.Instance.DownloadFile("pic.png",Application.persistentDataPath + "/DownloadPic.png", () => {
            print("Finish downloading");
        });

        FTPMgr.Instance.DeleteFile("Test.txt", (result) => {
            print(result? "Delete success" : "Delete failed");
        });
        
        FTPMgr.Instance.GetFileSize("pic.png", (result) => {
            print("Size = " + result);
        });
    }
}
