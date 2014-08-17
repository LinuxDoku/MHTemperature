using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MHTemperature;
using MHTemperature.Contracts;

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
				UpdateTemperature();
				RedrawTemperature();
			}

			return base.OnOptionsItemSelected(item);
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetupTemperatureService();

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			UpdateTemperature();
			RedrawTemperature();
		}

		protected void SetupTemperatureService()
		{
			TemperatureService = new TemperatureService();
		}

		protected void UpdateTemperature() 
		{
			if(TemperatureService != null) {
				LastTemperature = TemperatureService.Current();
			}
		}

		protected void RedrawTemperature()
		{
			if(LastTemperature != null) {
				RedrawTemperatureText(Resource.Id.SwimmerTemperature, LastTemperature.Swimmer);
				RedrawTemperatureProgress(Resource.Id.SwimmerTemperatureProgress, LastTemperature.Swimmer);
			}
		}

		protected void RedrawTemperatureProgress(string viewId, float value)
		{
			var progress = (ProgressBar)FindViewById(viewId);
			if(progress != null) {
				progress.Max = 50;
				progress.Progress = (int)value;
			}
		}

		protected void RedrawTemperatureText(string viewId, float value) {
			var text = (TextView)FindViewById(viewId);
			if(text != null) {
				text.Text = Formatter.FormatTemperature(value);
			}
		}
	}
}


