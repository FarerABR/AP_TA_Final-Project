using System;

namespace DAL.Entity.User
{
	public class User
	{
		public int Id { get; set; }

		public string Full_Name { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public User(int _Id, string _Full_Name, string _Username, string _Password)
		{
			Id = _Id;
			Full_Name = _Full_Name;
			Username = _Username;
			Password = _Password;
		}
	}
}
