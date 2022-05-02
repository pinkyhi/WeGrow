namespace WeGrow.Models.Requests
{
    public class UploadedFile
    {
        public byte[] FileContent { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
    }
}
