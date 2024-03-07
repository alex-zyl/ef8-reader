namespace Demo.Models;

public class Features
{
	public int Id { get; set; }

	public string MrId { get; set; }

	public int ZoomLevel { get; set; }

	public FeatureType Type { get; set; }

	public string FeatureData { get; set; }
}

public enum FeatureType
{
	Line = 0,
	Location = 1,
	TsoBorder = 2,
	CountryBorder = 3,
}