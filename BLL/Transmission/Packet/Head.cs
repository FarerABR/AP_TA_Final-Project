
namespace BLL.Transmission.Packet
{
    /// <summary>
    /// Header Packet - 5 byte
    /// </summary>
    /// 
    [Serializable]
    public class Head
    {
        /// <summary>
        /// Packet Id
        /// </summary>
        public int Id { get; set; } // 4 byte
        /// <summary>
        /// Type of packet
        /// </summary>
        public PacketType Type { get; set; } // 1 byte
    }
}
