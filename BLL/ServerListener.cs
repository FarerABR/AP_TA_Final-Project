using System;
using System.Net.Sockets;
using System.Net;

namespace BLL
{
	public class ServerListener
	{
		public Socket ServerSck;
		public Socket MainSck;
		public IPEndPoint EndPoint;

		private bool IsRunnig = false;

		public void StartServer(string _Ip, string _port)
		{
			ServerSck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			EndPoint = new IPEndPoint(IPAddress.Parse(_Ip), Convert.ToInt32(_port));
			ServerSck.Bind(EndPoint);
			ServerSck.Listen(10);
			MainSck = ServerSck.Accept();
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

		public static List<String> IpList()
		{
			List<string> list = new List<string>();
			list.Add("local host");
			string myHost = System.Net.Dns.GetHostName();

			System.Net.IPHostEntry myIPs = System.Net.Dns.GetHostEntry(myHost);

			foreach (var item in myIPs.AddressList)
			{
				if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
					list.Add(item.ToString());
			}

			return list;
		}

	}
}
