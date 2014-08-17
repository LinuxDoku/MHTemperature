using System;
using MonoMac.AppKit;
using System.Timers;
using MHTemperature.Contracts;

namespace MHTemperature.MacAgent
{
	public class MainMenu
	{
		protected TemperatureService TemperatureService;
		protected Timer UpdateTimer;
		protected Timer TimestampTimer;
		protected ITemperature LastTemperature;

		protected string SwimmerText = 		"Schwimmerbecken  ";
		protected string NonSwimmerText = 	"Nichtschwimmerbecken  ";
		protected string KidsText = 		"Kinderplanschbecken  ";
		protected string LastUpdateText = 	"Letzte Aktualisierung: Nie";

		protected NSMenu Menu;
		protected NSMenuItem Swimmer;
		protected NSMenuItem NonSwimmer;
		protected NSMenuItem Kids;
		protected NSMenuItem LastUpdate;

		public MainMenu(NSMenu menu)
		{
			Menu = menu;
			TemperatureService = new TemperatureService();

			Swimmer = MenuItemFactory(SwimmerText);
			NonSwimmer = MenuItemFactory(NonSwimmerText);
			Kids = MenuItemFactory(KidsText);
			LastUpdate = MenuItemFactory(LastUpdateText);

			Menu.AddItem(Swimmer);
			Menu.AddItem(NonSwimmer);
			Menu.AddItem(Kids);
			Menu.AddItem(LastUpdate);

			menu.AddItem(new NSMenuItem("Quit", (a, b) => Environment.Exit(0)));

			// fill with data
			UpdateLastTemperature();

			// update every 15 minutes
			UpdateTimer = new Timer(1000 * 60 * 15);
			UpdateTimer.Elapsed += (sender, e) => {
				menu.InvokeOnMainThread(UpdateLastTemperature);
			};
			UpdateTimer.Start();

			// update the timestamp every minute
			TimestampTimer = new Timer(1000 * 60 * 1);
			TimestampTimer.Elapsed += (sender, e) => {
				menu.InvokeOnMainThread(UpdateTimestampUI);
			};
			TimestampTimer.Start();
		}

		protected NSMenuItem MenuItemFactory(string poolName) {
			var menuItem = new NSMenuItem();
			menuItem.Title = poolName;

			return menuItem;
		}

		protected void UpdateLastTemperature()
		{
			LastTemperature = TemperatureService.Current();

			UpdateTemperatureUI();
			UpdateTimestampUI();
		}

		protected void UpdateTemperatureUI()
		{
			if(LastTemperature != null) {
				Swimmer.Title = SwimmerText + Formatter.FormatTemperature(LastTemperature.Swimmer);
				NonSwimmer.Title = NonSwimmerText + Formatter.FormatTemperature(LastTemperature.NonSwimmer);
				Kids.Title = KidsText + Formatter.FormatTemperature(LastTemperature.KidSplash);
			}
		}

		protected void UpdateTimestampUI() 
		{
			if(LastTemperature != null) {
				LastUpdate.Title = LastUpdateText.Replace("Nie", Formatter.FormatDateTime(LastTemperature.DateTime));
			}
		}
	}
}

