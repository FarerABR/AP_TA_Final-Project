using System.Runtime.Serialization.Formatters.Binary;

namespace BLL.Transmission.Packet.PacketSerialize
{
	public class PacketSerializer : BinaryWriter
	{
		private MemoryStream _memoryStream;
		private BinaryFormatter _binaryFormatter;

		public PacketSerializer() : base()
		{
			_memoryStream = new MemoryStream();
			_binaryFormatter = new BinaryFormatter();
			OutStream = _memoryStream;
		}
		public void Serialize(object o)
		{
			_binaryFormatter.Serialize(_memoryStream, o);
		}
		public byte[] GetByte()
		{
			base.Close();
			return _memoryStream.ToArray();
		}
	}
}
