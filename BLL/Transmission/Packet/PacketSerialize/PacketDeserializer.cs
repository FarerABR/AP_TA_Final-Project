using System.Runtime.Serialization.Formatters.Binary;

namespace BLL.Transmission.Packet.PacketSerialize
{
    public class PacketDeserializer : BinaryReader
    {
        private BinaryFormatter _binaryFormatter;

        public PacketDeserializer(byte[] buffer, int count) 
            : base(new MemoryStream(buffer,0, count))
        {
            _binaryFormatter = new BinaryFormatter();
        }

        public T DeSerialize<T>()
        {
            T res = (T)_binaryFormatter.Deserialize(BaseStream);
            return res;
        }
       
    }
}
