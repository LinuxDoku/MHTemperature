using MHTemperature.Contracts;

namespace MHTemperature
{
	public class Temperature : ITemperature
	{
		public float Swimmer { get; set; }

		public float NonSwimmer { get; set; }

		public float KidSplash { get; set; }

		public System.DateTime DateTime { get; set; }
	}
}

