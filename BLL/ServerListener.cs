using System;
using System.Net.Sockets;
using System.Net;

namespace BLL
{
	public class ServerListener
	{
		private Socket ServerSck;
		private Socket MainSck;
		private IPEndPoint EndPoint;

		private bool IsRunnig = false;

		public void StartServer(string _Ip, int _port)
		{
			ServerSck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			EndPoint = new IPEndPoint(IPAddress.Parse(_Ip), _port);
			ServerSck.Bind(EndPoint);
			ServerSck.Listen(10);
			ServerSck.BeginAccept(AcceptCallBack, null);
		}

		public void ReStartServer()
		{
			if (IsRunnig)
			{
				IsRunnig = false;
			}
			ServerSck.BeginAccept(AcceptCallBack, null);
		}

		public void AcceptCallBack(IAsyncResult ar)
		{
			MainSck = ServerSck.EndAccept(ar);
			IsRunnig = true;
		}

		public void StopServer()
		{
			if (!IsRunnig)
			{
				return;
			}
			ServerSck.Close();
			MainSck.Close();
			IsRunnig = false;
		}

	}
}
