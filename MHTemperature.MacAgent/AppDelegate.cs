using MonoMac.Foundation;
using MonoMac.AppKit;

namespace MHTemperature.MacAgent
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		public AppDelegate()
		{
		}

		public override void FinishedLaunching(NSObject notification)
		{
			NSApplication.SharedApplication.ActivationPolicy = NSApplicationActivationPolicy.Accessory;

			// menu
			var menu = new NSMenu();

			// create status icon
			var icon = NSStatusBar.SystemStatusBar.CreateStatusItem(30);
			icon.Menu = menu;
			icon.Image = NSImage.FromStream(System.IO.File.OpenRead(NSBundle.MainBundle.ResourcePath + "/SwimmingPool.icns"));
			icon.HighlightMode = true;

			new MainMenu(menu);
		}
	}
}

