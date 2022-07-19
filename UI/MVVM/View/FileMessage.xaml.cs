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

        public FileMessage(Transfer trasfer,string filepath)
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
        }

        public DownloadQueue AddDownloadFile(BLL.Transmission.Packet.FileInfo fileInfo,int id)
        {
            DownloadQueue downloadQueue = new DownloadQueue(id, fileInfo, Progress_Changed);
            _id = downloadQueue.Id;
            _filePath = downloadQueue.FilePath;
            txtb_Name.Text = fileInfo.Name + fileInfo.Extention;
            txtb_Time.Text = fileInfo.SendTime.ToShortTimeString();
            HorizontalAlignment = HorizontalAlignment.Left;
            btn_Attach.IsEnabled = false;
            ProcessPacket.Queues.Add(_id, downloadQueue);
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
                    Dispatcher.Invoke(() => {
                        btn_Attach.IsEnabled = true;
                        // ButtonProgressAssist.SetOpacity(AttachBtn, 0);
                    });
                }
            }
        }

        public void ProgressBar(int value)
        {
            Dispatcher.Invoke(() => { 
                // ButtonProgressAssist.SetValue(AttachBtn, value);
                _preogress = value;
            });
        }
    }
}
