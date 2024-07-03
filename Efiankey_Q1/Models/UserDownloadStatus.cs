namespace Efiankey_Q1.Models
{
    public class UserDownloadStatus
    {
        public string MemberType { get; set; }
        public int DownloadCount { get; set; }
        public DateTime LastDownloadTime { get; set; }


        public UserDownloadStatus(string memberType)
        {
            MemberType = memberType;
            DownloadCount = 0;
            LastDownloadTime = DateTime.MinValue;
        }
    }
}
