using System;
using System.Collections.Generic;
using System.Windows.Controls;
using BLL;
using System.Threading;
using UI.MVVM.View;

namespace UI.MVVM.View
{
	/// <summary>
	/// Interaction logic for ServerConfigView.xaml
	/// </summary>
	public partial class ServerConfigView : UserControl
	{
		public ServerConfigView()
		{
			InitializeComponent();
			ComboIp();
		}

		private void ComboIp()
		{
			List<String> list = Server.GetIpList();

			foreach (String ip in list)
			{
				cbox_IpBox.Items.Add(new ComboBoxItem() { Content = ip });
			}
			cbox_IpBox.SelectedIndex = 0;
		}

		private void btn_Server_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			string ip = (string)cbox_IpBox.SelectionBoxItem;
			string port = txt_Port.Text;

			if (ip == "local host")
			{
				ip = "127.0.0.1";
			}

			Server server = new Server(SocketAccepted_Handler);
			try
			{
				txtb_Status.Text = "Waiting for client...";
				ConnectionStatus(false);
				server.Start(ip, Convert.ToInt32(port));
				
				Thread.Sleep(5000);

				MainWindow mainWindow = new MainWindow(ConfigWindow._user, server, null);
				mainWindow.Show();

				ConfigWindow.closeWindow();

			}
			catch (Exception ex)
			{
				txtb_Status.Text = ex.Message;
				ConnectionStatus(true);
			}

		}

		public void SocketAccepted_Handler(object sender, AcceptedSocket e)
		{

		}

		private void ConnectionStatus(bool value)
		{
			cbox_IpBox.IsEnabled = value;
			txt_Port.IsEnabled = value;
			btn_Server.IsEnabled = value;
		}
	}
}
