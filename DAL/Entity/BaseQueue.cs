using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public delegate void ProgressChange(BaseQueue queue);
    public class BaseQueue
    {
        public const string RootPath = PacketConfig.DowloadPath;
        public string _filePath;
        public FileInfo _fileInfo;
        public FileStream _fileStream;
        public int _id;

        public int _fileindex = 0;
        public long Trasfered = 0;
        public int Progress = 0;
        public int LastProgress = 0;

        public ProgressChange _progressChanged;
        public int Id { get { return _id; } }
        public int ProgressFile { get { return LastProgress; } }
        public string FilePath { get { return _filePath; } }
        public FileInfo FileInfo { get { return _fileInfo; } }


    }
}
