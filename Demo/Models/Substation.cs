using System.Collections.Generic;

namespace Demo.Models;

public class Substation
{
	public int Id { get; set; }

	public string MrId { get; set; }

	public string Name { get; set; }

	public ICollection<VoltageMeasurement> VoltageMeasurement { get; set; }
}