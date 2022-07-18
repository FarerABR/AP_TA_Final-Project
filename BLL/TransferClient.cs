using System;
using System.Net;
using System.Net.Sockets;

namespace BLL
{
	public class TransferClient
	{
		public Socket _client;
		public IPEndPoint _iPEndPoint;
		public bool _isRunning = false;

		public void StartConnect(string ip, string port)
		{
			_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_client.Connect(ip, Convert.ToInt32(port));
			// _client.BeginConnect(ip, Convert.ToInt32(port), ConnectCallBack, null);
		}

		public void ConnectCallBack(IAsyncResult ar)
		{
			_client.EndConnect(ar);
			_iPEndPoint = (IPEndPoint)_client.RemoteEndPoint;
			_isRunning = true;
		}

		public void Close()
		{
			if (!_isRunning)
				return;
			_client.Close();
			_isRunning = false;
		}
	}
}
