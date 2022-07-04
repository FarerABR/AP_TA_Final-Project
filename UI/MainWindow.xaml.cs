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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL;

namespace UI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Rbtn_Home_Click(this, null);
		}


		private void brd_Head_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

		private void btn_Exit_Click(object sender, RoutedEventArgs e)
		{
			UserRepository.SaveData();
			this.Close();
		}

		private void btn_Minimize_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized;
		}

		private void Rbtn_Home_Click(object sender, RoutedEventArgs e)
		{
			Rbtn_Home.IsChecked = true;

			stk_Home.Children.Clear();

			ResourceDictionary resourceDictionary = new ResourceDictionary();
			resourceDictionary.Source = new Uri("/Styles/HomeBtnStyle.xaml", UriKind.RelativeOrAbsolute);
			Style btnStyle = resourceDictionary["HomeBtnStyle"] as Style;

			Button vidBtn = new Button();
			vidBtn.Content = "Video";
			vidBtn.Name = "btn_Video";
			vidBtn.Style = btnStyle;

			Button audBtn = new Button();
			audBtn.Content = "Audio";
			vidBtn.Name = "btn_Audio";
			audBtn.Style = btnStyle;

			Button imgBtn = new Button();
			imgBtn.Content = "Image";
			vidBtn.Name = "btn_Image";
			imgBtn.Style = btnStyle;

			Button allBtn = new Button();
			allBtn.Content = "All Files";
			vidBtn.Name = "btn_AllFiles";
			allBtn.Style = btnStyle;


			stk_Home.Children.Add(vidBtn);
			stk_Home.Children.Add(audBtn);
			stk_Home.Children.Add(imgBtn);
			stk_Home.Children.Add(allBtn);
		}

		private void Rbtn_History_Click(object sender, RoutedEventArgs e)
		{
			stk_Home.Children.Clear();
		}

		private void Rbtn_More_Click(object sender, RoutedEventArgs e)
		{
			stk_Home.Children.Clear();
		}
	}
}
