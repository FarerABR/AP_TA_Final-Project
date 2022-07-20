
namespace DAL.Entity
{
    [Serializable]
    public class FileBody
    {
        public int Index { get; set; }
        public int Count { get; set; }
        public byte[] Body { get; set; } = new byte[PacketConfig.FileBody];
    }
}
