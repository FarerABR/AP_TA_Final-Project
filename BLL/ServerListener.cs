using System;
using System.Net.Sockets;
using System.Net;

namespace BLL
{
	public static class ServerListener
	{
		private static Socket ServerSck;
		private static Socket MainSck;
		private static IPEndPoint EndPoint;

		private static bool IsRunnig = false;

		public static void StartServer(string _Ip, int _port)
		{
			ServerSck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			EndPoint = new IPEndPoint(IPAddress.Parse(_Ip), _port);
			ServerSck.Bind(EndPoint);
			ServerSck.Listen(10);
			ServerSck.BeginAccept(AcceptCallBack, null);
		}

		public static void ReStartServer()
		{
			if (IsRunnig)
			{
				IsRunnig = false;
			}
			ServerSck.BeginAccept(AcceptCallBack, null);
		}

		public static void AcceptCallBack(IAsyncResult ar)
		{
			MainSck = ServerSck.EndAccept(ar);
			IsRunnig = true;
		}

		public static void StopServer()
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
