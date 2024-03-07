namespace Demo.Models;

public class VoltageMeasurement
{
	public int Id { get; set; }

	public string IOPointMrId { get; set; }

	public FeatureType Type { get; set; }

	public string Value { get; set; }

	public string Voltage { get; set; }

	public bool Valid { get; set; }

	public Substation Substation { get; set; }
	public int SubstationId { get; set; }
}