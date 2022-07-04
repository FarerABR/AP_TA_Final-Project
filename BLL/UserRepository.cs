using DAL.Entity.User;
using Newtonsoft.Json;

namespace BLL
{
	public static class UserRepository
	{

		/// <summary>
		/// User list
		/// </summary>
		private static List<User> UserList;

		/// <summary>
		/// Loads the data to the app
		/// </summary>
		public static void LoadData()
		{
			if (!File.Exists(@".\data\userdata.json"))
			{
				Directory.CreateDirectory(@".\data\");
				File.Create(@".\data\userdata.json");
				return;
			}

			string path = @".\data\userdata.json";
			string strData = File.ReadAllText(path);
			UserList = JsonConvert.DeserializeObject<List<User>>(strData);
		}

		/// <summary>
		/// Saves all data to the json file
		/// </summary>
		public static void SaveData()
		{
			string path = @".\data\userdata.json";
			string strData = JsonConvert.SerializeObject(UserList);
			File.WriteAllText(path, strData);

		}

		/// <summary>
		/// creates a user if its not already created
		/// </summary>
		/// <param name="_user"></param>
		public static void CreateUser(User _user)
		{
			if (UserList.Where(x => x.Username == _user.Username).Any())
			{
				throw new Exception($"User with username: {_user.Username} already exist");
			}
			UserList.Add(_user);
		}

		/// <summary>
		/// returns a user by username
		/// </summary>
		/// <param name="_username"></param>
		/// <returns>User or null</returns>
		public static User ReadUser(string _username)
		{
			return UserList.Where(x => x.Username == _username).FirstOrDefault();
		}

	}
}
