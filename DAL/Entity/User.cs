using System;

namespace DAL.Entity
{
	public class User
	{
		public int Id { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public User(int _Id, string _Username, string _Password)
		{
			Id = _Id;
			Username = _Username;
			Password = _Password;
		}
	}
}
