using System;
using System.Windows;
using System.Windows.Input;
using BLL;
using DAL.Entity;
using DAL.Enum;
using BLL.Transmission;
using UI.MVVM.View;
using BLL.Transmission.PacketSerialize;
using Microsoft.Win32;
using System.Threading;

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
                txtb_HeadStatus.Text = $"Connected to {client.ServerEndpoint.ToString()}";
            }
            transfer.RunReceive();
        }

        private void SocketAccepted_Handler(object sender, AcceptedSocket e)
        {
            _server = (Server)sender;
            transfer = new Transfer(_server, true, Message_Handler, File_Handler, connection_Failed);
            transfer.RunReceive();
        }

        private void Message_Handler(Message message)
        {
            Dispatcher.Invoke(() =>
            {
                TextMessage textMessage = new TextMessage(message.Body, message.SendTime.ToShortTimeString());
                textMessage.HorizontalAlignment = HorizontalAlignment.Left;

                AddElementToUi(textMessage);

            });
        }

        private DownloadQueue File_Handler(DAL.Entity.FileInfo fileInfo, int id)
        {
            DownloadQueue downloadQueue = null;
            Dispatcher.Invoke(() =>
            {
                FileMessage file = new FileMessage();
                downloadQueue = file.AddDownloadFile(_user, fileInfo, id);
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
            // MessageBox.Show(error.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Dispatcher.Invoke(() =>
            {
                txtb_HeadStatus.Text = "!Disconnected!";
            });

            if (_client != null)
            {
                Dispatcher.Invoke(() =>
                {
                    // TODO: connection error
                    ConfigWindow configWindow = new ConfigWindow(_user);
                    configWindow.Show();
                    this.Close();
                });
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
            this.Close();
        }

        private void btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Rbtn_Home_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rbtn_History_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow(_user);
            historyWindow.Show();
            Rbtn_History.IsChecked = false;
            Rbtn_Home.IsChecked = true;
        }

        private void Rbtn_More_Click(object sender, RoutedEventArgs e)
        {
            MoreWindow moreWimdow = new MoreWindow();
            moreWimdow.Show();
            Rbtn_More.IsChecked = false;
            Rbtn_Home.IsChecked = true;
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

        private void Send_File(string path)
        {
            Dispatcher.Invoke(() =>
            {
                FileMessage file = new FileMessage(_user, transfer, path);
                AddElementToUi(file);
            });
        }

        private void btn_Video_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Mp4 files (*.mp4)|*.mp4|MOV files (*.mov)|*.mov|Mkv files (*.mkv)|*.mkv|Mpg files (*.mpg)|*.mpg|Avi files (*.avi)|*.avi";
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    Send_File(filename);
                    Thread.Sleep(300);
                }
            }
        }

        private void btn_Audio_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Mp3 files (*.mp3)|*.mp3|AAC files (*.aac)|*.aac|Ogg files (*.ogg)|*.ogg|Wav files (*.wav)|*.wav|Mp2 files (*.mp2)|*.mp2";
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    Send_File(filename);
                    Thread.Sleep(300);
                }
            }
        }

        private void btn_Image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Jpeg files (*.jpg)|*.jpg|PNG files (*.png)|*.png|Tif files (*.tif)|*.tif|GIF files (*.gif)|*.gif|Svg files (*.svg)|*.svg|Bitmap files (*.bitmap)|*.bitmap";
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    Send_File(filename);
                    Thread.Sleep(300);
                }
            }
        }

        private void btn_AllFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                        Send_File(filename);
                        Thread.Sleep(300);
                }
            }
        }

    }
}
