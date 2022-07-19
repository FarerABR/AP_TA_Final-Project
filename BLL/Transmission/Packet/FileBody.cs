
namespace BLL.Transmission.Packet
{
    [Serializable]
    public class FileBody
    {
        public int Index { get; set; } /// 0|1|2|3| ... |1025|1026|1027|...|1050| | | | | 
        public int Count { get; set; }
        public byte[] Body { get; set; } = new byte[PacketConfig.FileBody];
    }
}
