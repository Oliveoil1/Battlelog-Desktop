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
	/// Interaction logic for Launcher.xaml
	/// </summary>
	public partial class Launcher : Window
	{
		public Launcher()
		{
			InitializeComponent();
		}

		private void GameButton_Click(object sender, RoutedEventArgs e)
		{
			string gameId = "bf4";
			
			if (sender == BF3Button)
			{
				gameId = "bf3";
			}
			else if (sender == BF4Button)
			{
				gameId = "bf4";
			}
			else if (sender == BFHButton)
			{
				gameId = "bfh";
			}
			else if (sender == MOHWButton)
			{
				gameId = "mohw";
			}

			StartGame(gameId);
		}

		private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void MinimizeWindowButton_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if(App.Args.Length == 2 && App.Args[0] == "-game")
			{
				if(App.Args[1] == "bf3" || App.Args[1] == "bf4" || App.Args[1] == "bfh" || App.Args[1] == "mohw")
				{
					StartGame(App.Args[1]);
				}
			}
		}

		private void StartGame(string gameId)
		{
			var window = new BrowserWindow();
			window.gameId = gameId;
			window.Show();
			Close();
		}
	}
}
