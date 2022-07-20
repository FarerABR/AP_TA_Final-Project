using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace UI
{
	/// <summary>
	/// Interaction logic for MoreWindow.xaml
	/// </summary>
	public partial class MoreWindow : Window
	{
		public MoreWindow()
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

		private void hl_Ripository_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
		{
			using (System.Diagnostics.Process p = new System.Diagnostics.Process())
			{
				p.StartInfo = new ProcessStartInfo(e.Uri.AbsoluteUri)
				{
					UseShellExecute = true,
				};
				p.Start();
			}
		}
	}
}
