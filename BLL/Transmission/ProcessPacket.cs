﻿using BLL.Transmission.PacketSerialize;
using DAL.Entity;
using DAL.Enum;

namespace BLL.Transmission
{
    public delegate void MessageHandler(Message message);
    public delegate DownloadQueue FileDowloadHandler(DAL.Entity.FileInfo fileInfo,int id);

    public class ProcessPacket
    {

        public static Dictionary<int, BaseQueue> Queues = new Dictionary<int, BaseQueue>();

        private event MessageHandler _messageHandler;
        private event FileDowloadHandler _fileDowloadHandler;

        public ProcessPacket(MessageHandler messageHandler, FileDowloadHandler fileDowloadHandler)
        {
            _messageHandler = messageHandler;
            _fileDowloadHandler = fileDowloadHandler;

        }


        public void Process(byte[] _buffer , int Validcount, Transfer transfer)
        {
            PacketDeserializer deserializer = new PacketDeserializer(_buffer, Validcount);
            Head head = deserializer.DeSerialize<Head>();
            switch (head.Type)
            {
                case PacketType.Start:
                    {
                        ((UploadQueue)Queues[head.Id]).Start();
                        break;
                    }
                case PacketType.FileInfo:
                    {
                        DAL.Entity.FileInfo fileInfo = deserializer.DeSerialize<DAL.Entity.FileInfo>();
                        DownloadQueue download = _fileDowloadHandler(fileInfo, head.Id);
                        Head header = new Head()
                        {
                            Id = head.Id,
                            Type = PacketType.Start
                        };
                        PacketSerializer packetSerializer = new PacketSerializer();
                        packetSerializer.Serialize(header);
                        transfer.Send(packetSerializer.GetByte());



                        break;
                    }
                case PacketType.FileBody:
                    {
                        FileBody fileBody = deserializer.DeSerialize<FileBody>();
                        ((DownloadQueue)Queues[head.Id]).Write(fileBody);
                        break;
                    }
                case PacketType.Message:
                    {
                        Message message = deserializer.DeSerialize<Message>();
                        _messageHandler(message);
                        break;
                    }
                default:
                    throw new Exception("Something went wrong!");
            }
        }
    }
}
