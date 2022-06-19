using System;

namespace DAL.Entity.User
{
	public class User
	{
		private int Id { get; set; }

		private string Full_Name { get; set; }

		private string Username { get; set; }

		private string Password { get; set; }

		public User(int _Id, string _Full_Name, string _Username, string _Password)
		{
			Id = -Id;
			Full_Name = _Full_Name;
			Username = _Username;
			Password = _Password;
		}
	}
}
