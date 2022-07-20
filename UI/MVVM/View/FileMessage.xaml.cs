using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Transmission;
using System.Diagnostics;
using DAL.Entity;
using System.Threading;

namespace UI.MVVM.View
{
    /// <summary>
    /// Interaction logic for FileMessage.xaml
    /// </summary>
    public partial class FileMessage : UserControl
    {
        private string _filePath;
        private int _preogress = 0;
        private int _id;

        public FileMessage()
        {
            InitializeComponent();
        }

        public FileMessage(User user, Transfer trasfer, string filepath)
        {
            InitializeComponent();
            _filePath = filepath;
            UploadQueue upload = new UploadQueue(trasfer, _filePath, Progress_Changed);
            upload.SendFileInfo();
            _id = upload.Id;
            txtb_Name.Text = upload.FileInfo.Name + upload.FileInfo.Extention;
            txtb_Time.Text = upload.FileInfo.SendTime.ToShortTimeString();
            HorizontalAlignment = HorizontalAlignment.Right;

            ProcessPacket.Queues.Add(upload.Id, upload);

            user.Packets.Add(new Packets()
            {
                PacketType = "Upload",
                Id = upload.Id,
                Name = upload.FileInfo.Name,
                Extention = upload.FileInfo.Extention,
                Size = upload.FileInfo.Size,
                SendTime = upload.FileInfo.SendTime
            });

            // this is for history
        }

        public DownloadQueue AddDownloadFile(User user, DAL.Entity.FileInfo fileInfo, int id)
        {
            DownloadQueue downloadQueue = new DownloadQueue(id, fileInfo, Progress_Changed);
            _id = downloadQueue.Id;
            _filePath = downloadQueue.FilePath;
            txtb_Name.Text = fileInfo.Name + fileInfo.Extention;
            txtb_Time.Text = fileInfo.SendTime.ToShortTimeString();
            HorizontalAlignment = HorizontalAlignment.Left;
            btn_Attach.IsEnabled = false;
            ProcessPacket.Queues.Add(_id, downloadQueue);

            // this is for history
            user.Packets.Add(new Packets()
            {
                PacketType = "Download",
                Id = downloadQueue.Id,
                Name = downloadQueue.FileInfo.Name,
                Extention = downloadQueue.FileInfo.Extention,
                Size = downloadQueue.FileInfo.Size,
                SendTime = downloadQueue.FileInfo.SendTime
            });
            return downloadQueue;
        }

        private void Progress_Changed(BaseQueue queue)
        {
            if (queue != null)
            {
                ProgressBar(queue.ProgressFile);
                if (queue.ProgressFile == 100)
                {
                    _filePath = queue.FilePath;
                    ProcessPacket.Queues.Remove(queue.Id);
                    Dispatcher.Invoke(() =>
                    {
                        btn_Attach.IsEnabled = true;
                        stk_Btn.Children.Remove(txtb_Progress);
                        stk_Name.Children.Remove(prb_Progress);
                    });
                }
            }
        }

        public void ProgressBar(int value)
        {
            Dispatcher.Invoke(() =>
            {
                prb_Progress.Value = value;
                txtb_Progress.Text = value.ToString() + "%";
                _preogress = value;
            });
        }

        private void btn_Attach_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (System.Diagnostics.Process p = new System.Diagnostics.Process())
                {
                    p.StartInfo = new ProcessStartInfo()
                    {
                        UseShellExecute = true,
                        FileName = _filePath,
                    };
                    p.Start();
                }
            }
            catch
            {
                btn_Attach.IsEnabled = false;
                MessageBox.Show("File not found.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
