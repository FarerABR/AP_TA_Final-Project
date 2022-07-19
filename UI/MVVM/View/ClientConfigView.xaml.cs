using System;
using System.Collections.Generic;
using System.Windows.Controls;
using BLL;
using System.Threading;
using UI.MVVM.View;

namespace UI.MVVM.View
{
	/// <summary>
	/// Interaction logic for ClientConfigView.xaml
	/// </summary>
	public partial class ClientConfigView : UserControl
	{
		public ClientConfigView()
		{
			InitializeComponent();
		}

		private void btn_Client_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			string ip = txt_Ip.Text;
			string port = txt_Port.Text;

			if (ip == "local host")
			{
				ip = "127.0.0.1";
			}

			Client client = new Client(Disconnected_Handler);
			try
			{
				txtb_Status.Text = "Connecting to server...";
				ConnectionStatus(false);
				client.Connect(ip, Convert.ToInt32(port), Connected_Handler);

				Thread.Sleep(5000);

				MainWindow mainWindow = new MainWindow(ConfigWindow._user, null, client);
				mainWindow.Show();

				ConfigWindow.closeWindow();
			}
			catch (Exception ex)
			{
				txtb_Status.Text = ex.Message;
				ConnectionStatus(true);
			}
		}

		public void Disconnected_Handler(object sender)
		{

		}
		public void Connected_Handler(object sender, string error)
		{

		}

		private void ConnectionStatus(bool value)
		{
			txt_Ip.IsEnabled = value;
			txt_Port.IsEnabled = value;
			btn_Client.IsEnabled = value;
		}
	}
}
