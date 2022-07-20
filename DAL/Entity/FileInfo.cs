
namespace DAL.Entity
{
    [Serializable]
    public class FileInfo
    {
        public string Name { get; set; }
        public string Extention { get; set; }
        public long Size { get; set; }
        public DateTime SendTime { get; set; }
    }
}
