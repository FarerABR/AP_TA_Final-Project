using System;

namespace DAL.Entity
{
	public class User
	{
		private int _id;
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _username;
		public string Username
		{
			get { return _username; }
			set { _username = value; }
		}

		private string _password;
		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		private List<Packets> _packets;
		public List<Packets> Packets
		{
			get { return _packets; }
			set { _packets = value; }
		}

		public User(int _Id, string _Username, string _Password)
		{
			Id = _Id;
			Username = _Username;
			Password = _Password;
			Packets = new List<Packets>();
		}
	}
}
