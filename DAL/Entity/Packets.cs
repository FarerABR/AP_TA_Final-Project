using System;

namespace DAL.Entity
{
    public class Packets
    {
        private string  _packetType;

        private int _id;

        private string _name;
        
        private string _extention;
        
        private long _size;
        
        private DateTime _sendTime;




        public string  PacketType
        {
            get { return _packetType; }
            set { _packetType = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Extention
        {
            get { return _extention; }
            set { _extention = value; }
        }
        
        public long Size
        {
            get { return _size; }
            set { _size = value; }
        }
        
        public DateTime SendTime
        {
            get { return _sendTime; }
            set { _sendTime = value; }
        }
        
    }
}
