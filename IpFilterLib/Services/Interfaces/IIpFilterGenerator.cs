using IpFilterLib.Models;

namespace IpFilterLib.Services.Interfaces;
public interface IIpFilterGenerator
{
    public List<IpRange> FilterByCountry(List<IpRange> ipRanges, List<string> countryCodes);

    public void WriteIpFilteredFile(List<IpRange> ipRanges, string outputPath);
}
