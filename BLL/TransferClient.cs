using System;
using System.Net;
using System.Net.Sockets;

namespace BLL
{
	public class TransferClient
	{
		public Socket clientSck;
		public IPEndPoint _iPEndPoint;
		public bool _isRunning = false;

		public void StartConnect(string ip, string port)
		{
			clientSck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			clientSck.Connect(ip, Convert.ToInt32(port));
			// clientSck.BeginConnect(ip, Convert.ToInt32(port), ConnectCallBack, null);
		}

		public void ConnectCallBack(IAsyncResult ar)
		{
			clientSck.EndConnect(ar);
			_iPEndPoint = (IPEndPoint)clientSck.RemoteEndPoint;
			_isRunning = true;
		}

		public void Close()
		{
			if (!_isRunning)
				return;
			clientSck.Close();
			_isRunning = false;
		}
	}
}
