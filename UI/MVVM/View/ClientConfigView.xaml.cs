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
		private Client _client;
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

			_client = new Client(Disconnected_Handler);
			try
			{
				txtb_Status.Text = "Connecting to server...";
				ConnectionStatus(false);
				_client.Connect(ip, Convert.ToInt32(port), Connected_Handler);



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
			Dispatcher.Invoke(() =>
			{
				MainWindow mainWindow = new MainWindow(ConfigWindow._user, null, _client);
				mainWindow.Show();

				ConfigWindow.closeWindow();
			});
		}

		private void ConnectionStatus(bool value)
		{
			txt_Ip.IsEnabled = value;
			txt_Port.IsEnabled = value;
			btn_Client.IsEnabled = value;
		}
	}
}
