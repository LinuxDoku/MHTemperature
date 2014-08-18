using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MHTemperature;
using MHTemperature.Contracts;
using Java.Util;
using System.Collections.Generic;

namespace MHTemperature.Android
{
	[Activity(Label = "MHTemperature.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected ITemperatureService TemperatureService;
		protected ITemperature LastTemperature;

		public override bool OnPrepareOptionsMenu(IMenu menu) {
			MenuInflater.Inflate(Resource.Menu.MainActivityMenu, menu);
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if(item.ItemId == Resource.Id.ActionRefresh) {
				UpdateLastTemperature();
			}

			return base.OnOptionsItemSelected(item);
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);

			SetupTemperatureService();
			UpdateLastTemperature();
		}

		protected void SetupTemperatureService()
		{
			TemperatureService = new TemperatureService();
		}

		protected void UpdateLastTemperature() 
		{
			if(TemperatureService != null) {
				LastTemperature = TemperatureService.Current();

				UpdateTemperatureUI();
			}
		}

		protected void UpdateTemperatureUI()
		{
			if(LastTemperature != null) {
				var UpdateTextMap = new Dictionary<int, float>() {
					{ Resource.Id.SwimmerTemperatureText, LastTemperature.Swimmer },
					{ Resource.Id.NonSwimmerTemperatureText, LastTemperature.NonSwimmer },
					{ Resource.Id.KidsTemperatureText, LastTemperature.KidSplash },
				};

				foreach(var map in UpdateTextMap) {
					RedrawTemperatureText(map.Key, map.Value);
				}

				var UpdateProgressMap = new Dictionary<int, float>() {
					{ Resource.Id.SwimmerTemperatureProgress, LastTemperature.Swimmer },
					{ Resource.Id.NonSwimmerTemperatureProgress, LastTemperature.NonSwimmer },
					{ Resource.Id.KidsTemperatureProgress, LastTemperature.KidSplash },
				};

				foreach(var map in UpdateProgressMap) {
					RedrawTemperatureProgress(map.Key, map.Value);
				}
			}
		}

		protected void RedrawTemperatureProgress(int viewId, float temperature)
		{
			var progress = (ProgressBar)FindViewById(viewId);
			if(progress != null) {
				progress.Max = 40;
				progress.Progress = (int)temperature;
			}
		}

		protected void RedrawTemperatureText(int viewId, float temperature) {
			var text = (TextView)FindViewById(viewId);
			if(text != null) {
				text.Text = Formatter.FormatTemperature(temperature);
			}
		}
	}
}


