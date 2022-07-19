using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace BLL.Transmission
{
	public delegate void ConnectionFailed(Exception error);
	public class Transfer
	{
		private const int _bufferSize = PacketConfig.PacketSize;
		private byte[] _buffer;
		private Socket _socket;
		private ProcessPacket _processPacket;
		private event ConnectionFailed _connectionFailed;

		public Transfer(Socket socket, bool canTransfer
			, MessageHandler message_Handler
			, FileDowloadHandler file_Handler
			, ConnectionFailed connectionFailed
			)
		{
			_socket = socket;
			_processPacket = new ProcessPacket(message_Handler, file_Handler);
			_buffer = new byte[_bufferSize];
			_connectionFailed = connectionFailed;
			if (!Directory.Exists(PacketConfig.DowloadPath))
			{
				Directory.CreateDirectory(PacketConfig.DowloadPath);
			}

		}
		public void Send(byte[] buffer)
		{
			// if (!_socket.IsRunning)
			//    return;
			try
			{
				_socket.Send(BitConverter.GetBytes(buffer.Length), 0, 4, SocketFlags.None);
				_socket.Send(buffer, 0, buffer.Length, SocketFlags.None);
			}
			catch
			{
				_socket.Close();
			}
		}


		public void RunReceive()
		{
			try
			{
				_socket.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.Peek, receiveCallback, null);
			}
			catch (System.Exception ex)
			{
				throw new Exception("here"+ex.ToString());
			}

		}
		private void receiveCallback(IAsyncResult ar)
		{
			try
			{
				int ReceiveSize = _socket.EndReceive(ar);
				if (ReceiveSize >= 4)
				{
					_socket.Receive(_buffer, 0, 4, SocketFlags.None);
					int size = BitConverter.ToInt32(_buffer, 0);

					int read = _socket.Receive(_buffer, 0, size, SocketFlags.None);

					while (read < size)
					{
						read += _socket.Receive(_buffer, read, size - read, SocketFlags.None);
					}

					_processPacket.Process(_buffer, size, this);
				}
				RunReceive();
			}
			catch (Exception ex)
			{
				 _socket.Close();
				_connectionFailed(ex);
			}
		}
	}
}
