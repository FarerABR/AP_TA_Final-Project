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
using System.Windows.Shapes;
using BLL;
using DAL.Entity.User;

namespace UI
{
	/// <summary>
	/// Interaction logic for StartupWindow.xaml
	/// </summary>
	public partial class StartupWindow : Window
	{
		public StartupWindow()
		{
			InitializeComponent();
		}

		private void brd_Head_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

		private void btn_Minimize_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized;
		}

		private void btn_Exit_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void btn_SignUp_Click(object sender, RoutedEventArgs e)
		{
			Random rnd = new Random(10);
			User user = new User(rnd.Next(), txtb_Username.Text, txtb_Password.Text);

			try
			{
				UserRepository.CreateUser(user);
			}
			catch (Exception ex)
			{
				lbl_Error.Content = ex.Message;
			}
		}

		private void btn_LoginPage_Click(object sender, RoutedEventArgs e)
		{
			User user = UserRepository.ReadUser(txtb_Username.Text);
			if (user == null)
			{
				lbl_Error.Content = "User Not Found!";
				return;
			}

			if (txtb_Password.Text != user.Password)
			{
				lbl_Error.Content = "Wrong Password!";
				return;
			}

			// TODO: this is where i send the user to config window
		}
	}
}
