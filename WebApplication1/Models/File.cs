namespace WebApplication1.Models
{
    public class File
    {
        public int FileID { get; set; }
        public int TeamID { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileSize { get; set; }
        public virtual Team Team { get; set; }
    }
}