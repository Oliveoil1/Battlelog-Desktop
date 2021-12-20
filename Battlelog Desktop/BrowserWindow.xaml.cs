using DiscordRPC;
using DiscordRPC.Logging;
using Microsoft.Web.WebView2.Core;
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
	/// Interaction logic for Browserxaml
	/// </summary>
	/// 

	public partial class BrowserWindow : Window
	{
		public string gameId = "gameId";
		DiscordRpcClient client;

		public BrowserWindow()
		{
			InitializeComponent();
			InitializeBrowser();
		}

		private async void InitializeBrowser()
		{
			var userDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Battlelog Desktop";
			var env = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
			await BrowserView.EnsureCoreWebView2Async(env);
		}

		private void BrowserView_Loaded(object sender, RoutedEventArgs e)
		{
			BrowserView.DefaultBackgroundColor = System.Drawing.Color.Transparent;
			BrowserView.Source = new Uri($"https://accounts.ea.com/connect/auth?locale=en_US&state={gameId}&redirect_uri=https%3A%2F%2Fbattlelog.battlefield.com%2Fsso%2F%3Ftokentype%3Dcode&response_type=code&client_id=battlelog&display=web%2Flogin");
		}

		private async void InjectJS()
		{
			await BrowserView.CoreWebView2.ExecuteScriptAsync(LoadScript("/Inject/Scripts/InjectStyle.js"));
		}
		
		private string LoadScript(string path, bool relative = true)
		{
			if (relative == true)
			{
				path = AppDomain.CurrentDomain.BaseDirectory + path;
			}
			return System.IO.File.ReadAllText(path); ;
		}

		private void SetStatus(string gameId)
		{
			if (client != null)
			{
				client.Deinitialize();
				client = null;
			}

			string appId = "appId";

			switch (gameId)
			{
				case "bf3":
					appId = "917674887852736513";
					break;
				case "bf4":
					appId = "917674515713097728";
					break;
				case "bfh":
					appId = "917674939954376714";
					break;
				case "mohw":
					appId = "917675003347099648";
					break;
			}

			client = new DiscordRpcClient(appId);

			client.Initialize();
		}

		private void BrowserView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
		{
			BackgroundImage.ImageSource = null;
			//Title = BrowserView.CoreWebView2.DocumentTitle;
			ToolbarMenu.Visibility = Visibility.Visible;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ToolbarMenu.Visibility = Visibility.Collapsed;
			BackgroundImage.ImageSource = new BitmapImage(new Uri($"pack://Application:,,,/Images/Backgrounds/{gameId}_background.jpg"));

			SetStatus(gameId);
		}

		private void ServerBrowserMenu_Click(object sender, RoutedEventArgs e)
		{
			if (sender == HomeMenu)
			{
				BrowserView.Source = new Uri($"https://battlelog.battlefield.com/{gameId}");
			}
			else if (sender == ServerBrowserMenu)
			{
				BrowserView.Source = new Uri($"https://battlelog.battlefield.com/{gameId}/servers");
			}
			else if (sender == CampaignMenu)
			{
				BrowserView.Source = new Uri($"https://battlelog.battlefield.com/{gameId}/campaign");
			}
		}

		private void GameSwitchMenu_Click(object sender, RoutedEventArgs e)
		{
			gameId = (sender as MenuItem).Tag.ToString();

			BF3SwitchMenu.IsChecked = false;
			BF4SwitchMenu.IsChecked = false;
			BFHSwitchMenu.IsChecked = false;
			MOHWSwitchMenu.IsChecked = false;

			switch (gameId)
			{
				case "bf3":
					BF3SwitchMenu.IsChecked = true;
					Title = "Battlelog - Battlefield 3";
					break;
				case "bf4":
					BF4SwitchMenu.IsChecked = true;
					Title = "Battlelog - Battlefield 4";
					break;
				case "bfh":
					BFHSwitchMenu.IsChecked = true;
					Title = "Battlelog - Battlefield Hardline";
					break;
				case "mohw":
					MOHWSwitchMenu.IsChecked = true;
					Title = "Battlelog - Medal of Honor Warfighter";
					break;
				default:
					Title = "Battelog Desktop";
					break;
			}

			BrowserView.Source = new Uri($"https://battlelog.battlefield.com/{gameId}");
			SetStatus(gameId);
		}

		private void NavigationButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender == BackButton)
			{
				BrowserView.GoBack();
			}
			else if (sender == ForwardButton)
			{
				BrowserView.GoForward();
			}
		}

		private void BrowserView_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
		{
			InjectJS();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			client.Deinitialize();
		}
	}
}
