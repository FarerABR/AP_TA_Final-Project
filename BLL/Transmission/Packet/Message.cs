using System.ComponentModel.DataAnnotations;

namespace BLL.Transmission.Packet
{
    [Serializable]
    public class Message
    {
        [MaxLength(10200)]
        public string Body { get; set; }
        public DateTime SendTime { get; set; }
    }
}
