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
			string gameId = (sender as Button).Tag.ToString();

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
				StartGame(App.Args[1]);
			}
		}

		private void StartGame(string gameId)
		{
			var window = new BrowserWindow();

			switch(gameId)
			{
				case "bf3":
					window.BF3SwitchMenu.IsChecked = true;
					window.Title = "Battlelog - Battlefield 3";
					break;
				case "bf4":
					window.BF4SwitchMenu.IsChecked = true;
					window.Title = "Battlelog - Battlefield 4";
					break;
				case "bfh":
					window.BFHSwitchMenu.IsChecked = true;
					window.Title = "Battlelog - Battlefield Hardline";
					break;
				case "mohw":
					window.MOHWSwitchMenu.IsChecked = true;
					window.Title = "Battlelog - Medal of Honor Warfighter";
					break;
				default:
					MessageBox.Show("Invalid game id");
					break;
			}
			window.gameId = gameId;

			window.Show();
			Close();
		}
	}
}
