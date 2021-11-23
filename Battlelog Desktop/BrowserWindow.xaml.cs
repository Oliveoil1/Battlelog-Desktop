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

namespace Battlelog_Desktop
{
	/// <summary>
	/// Interaction logic for BrowserWindow.xaml
	/// </summary>
	/// 

	public partial class BrowserWindow : Window
	{
		public string gameId = "gameId";

		public BrowserWindow()
		{
			InitializeComponent();
		}

		private void BrowserView_Loaded(object sender, RoutedEventArgs e)
		{
			BrowserView.DefaultBackgroundColor = System.Drawing.Color.Transparent;
			BrowserView.Source = new Uri($"https://accounts.ea.com/connect/auth?locale=en_US&state={gameId}&redirect_uri=https%3A%2F%2Fbattlelog.battlefield.com%2Fsso%2F%3Ftokentype%3Dcode&response_type=code&client_id=battlelog&display=web%2Flogin");
		}

		private void BrowserView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
		{
			BackgroundImage.ImageSource = null;
			Title = BrowserView.CoreWebView2.DocumentTitle;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			BackgroundImage.ImageSource = new BitmapImage(new Uri($"pack://Application:,,,/Images/Backgrounds/{gameId}_background.jpg"));
		}
	}
}
