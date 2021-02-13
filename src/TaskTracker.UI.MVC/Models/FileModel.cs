namespace TaskTracker.UI.MVC.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
        public string ContentType { get; set; }
    }
}