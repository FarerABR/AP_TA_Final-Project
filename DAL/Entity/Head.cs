using DAL.Enum;

namespace DAL.Entity
{
    [Serializable]
    public class Head
    {
        public int Id { get; set; } // 4 byte

        public PacketType Type { get; set; } // 1 byte
    }
}
