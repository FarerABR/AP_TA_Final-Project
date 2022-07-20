using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    [Serializable]
    public class Message
    {
        [MaxLength(10200)]
        public string Body { get; set; }
        public DateTime SendTime { get; set; }
    }
}
