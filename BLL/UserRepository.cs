using System;
using DAL.Entity.User;
using SQLite;

namespace BLL
{
	public class UserRepository
	{
		private SQLiteConnection Connection;

		public void LoadDB()
		{
			if (!File.Exists(@".\UserDB.db3"))
			{
				Connection = new SQLiteConnection(@".\UserDB.db3");
				Connection.CreateTable<User>();
			}
		}

		public bool CreateUser(User user)
		{
			Connection.Insert(user);
			return true;
		}


	}
}
