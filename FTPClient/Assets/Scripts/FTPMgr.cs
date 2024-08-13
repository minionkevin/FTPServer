

public class FTPMgr
{
    private static FTPMgr instance = new FTPMgr();
    public static FTPMgr Instance => instance;

    private string FTP_PATH = "ftp://127.0.0.1";

    public async void UpLoadFile(string fileName, string localPath)
    {
        
    }
}
