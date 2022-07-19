using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BLL;
using DAL.Entity;
using System.Net;
using System.Net.Sockets;
using BLL.Transmission;
using UI.MVVM.View;
using BLL.Transmission.Packet;
using BLL.Transmission.Packet.PacketSerialize;

namespace UI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public User _user;
		public Server _server = null;
		public Client _client = null;
		public Transfer transfer;
		public MainWindow(User user, Server server, Client client)
		{
			InitializeComponent();
			Rbtn_Home_Click(this, null);
			_user = user;
			txtb_Username.Text = _user.Username;
			if (client == null)
			{
				transfer = new Transfer(server, true, Message_Handler, File_Handler, connection_Failed);
				_server = server;
				txtb_HeadStatus.Text = $"Started the server with ip: {server.Ip}:{server.Port}";
			}
			else
			{
				transfer = new Transfer(client, true, Message_Handler, File_Handler, connection_Failed);
				_client = client;
				txtb_HeadStatus.Text = $"Connected to {client.ServerEndpoint}";
			}
			transfer.RunReceive();
		}

		private void SocketAccepted_Handler(object sender, AcceptedSocket e)
		{
			_server = (Server)sender;
			transfer = new Transfer(_server, true, Message_Handler, File_Handler, connection_Failed);
			transfer.RunReceive();
		}

		private void Message_Handler(BLL.Transmission.Packet.Message message)
		{
			Dispatcher.Invoke(() =>
			{
				TextMessage textMessage = new TextMessage(message.Body, message.SendTime.ToShortTimeString());
				textMessage.HorizontalAlignment = HorizontalAlignment.Left;

				AddElementToUi(textMessage);

			});
		}

		private DownloadQueue File_Handler(BLL.Transmission.Packet.FileInfo fileInfo, int id)
		{
			DownloadQueue downloadQueue = null;
			Dispatcher.Invoke(() =>
			{
				FileMessage file = new FileMessage();
				downloadQueue = file.AddDownloadFile(fileInfo, id);
				AddElementToUi(file);
			});
			return downloadQueue;
		}

		private void AddElementToUi(object o)
		{
			if (!Dispatcher.CheckAccess())
			{
				Dispatcher.Invoke(() =>
				{
					stk_Chat.Children.Add((UIElement)o);
					scv_Chat.ScrollToEnd();
				});
				return;
			}
			else
			{
				stk_Chat.Children.Add((UIElement)o);
				scv_Chat.ScrollToEnd();
			}
		}

		private void connection_Failed(Exception error)
		{
			MessageBox.Show(error.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			Dispatcher.Invoke(() =>
			{
				txtb_HeadStatus.Text = "!Disconnected!";
			});

			if (_client != null)
			{
				// TODO: connection error
			}
			else if (_server != null)
			{
				// TODO: connection error
				_server.StartReAccept(SocketAccepted_Handler);

			}
		}



		private void brd_Head_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

		private void btn_Exit_Click(object sender, RoutedEventArgs e)
		{
			UserRepository.SaveData();
			this.Close();
		}

		private void btn_Minimize_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized;
		}

		private void Rbtn_Home_Click(object sender, RoutedEventArgs e)
		{
			Rbtn_Home.IsChecked = true;

			stk_Home.Children.Clear();

			ResourceDictionary resourceDictionary = new ResourceDictionary();
			resourceDictionary.Source = new Uri("/Styles/HomeBtnStyle.xaml", UriKind.RelativeOrAbsolute);
			Style btnStyle = resourceDictionary["HomeBtnStyle"] as Style;

			Button vidBtn = new Button();
			vidBtn.Content = "Video";
			vidBtn.Name = "btn_Video";
			vidBtn.Style = btnStyle;

			Button audBtn = new Button();
			audBtn.Content = "Audio";
			vidBtn.Name = "btn_Audio";
			audBtn.Style = btnStyle;

			Button imgBtn = new Button();
			imgBtn.Content = "Image";
			vidBtn.Name = "btn_Image";
			imgBtn.Style = btnStyle;

			Button allBtn = new Button();
			allBtn.Content = "All Files";
			vidBtn.Name = "btn_AllFiles";
			allBtn.Style = btnStyle;


			stk_Home.Children.Add(vidBtn);
			stk_Home.Children.Add(audBtn);
			stk_Home.Children.Add(imgBtn);
			stk_Home.Children.Add(allBtn);
		}

		private void Rbtn_History_Click(object sender, RoutedEventArgs e)
		{
			stk_Home.Children.Clear();
		}

		private void Rbtn_More_Click(object sender, RoutedEventArgs e)
		{
			stk_Home.Children.Clear();
		}

		private void btn_Send_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(txt_Send.Text.Trim()))
			{
				Message message = new Message()
				{
					Body = txt_Send.Text.Trim(),
					SendTime = DateTime.Now
				};

				Head head = new Head()
				{
					Id = new Random().Next(),
					Type = PacketType.Message
				};

				PacketSerializer packetSerializer = new PacketSerializer();
				packetSerializer.Serialize(head);
				packetSerializer.Serialize(message);
				transfer.Send(packetSerializer.GetByte());
				TextMessage textMessage = new TextMessage(message.Body, message.SendTime.ToShortTimeString());
				textMessage.HorizontalAlignment = HorizontalAlignment.Right;
				AddElementToUi(textMessage);

				txt_Send.Text = "";
			}
		}
	}
}
