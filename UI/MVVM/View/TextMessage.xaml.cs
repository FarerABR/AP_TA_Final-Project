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

namespace UI.MVVM.View
{
	/// <summary>
	/// Interaction logic for ServerTextMessage.xaml
	/// </summary>
	public partial class TextMessage : UserControl
	{
		public TextMessage(string text, string time)
		{
			InitializeComponent();
			txtb_Text.Text = text;
			txtb_Time.Text = time;
		}
	}
}
