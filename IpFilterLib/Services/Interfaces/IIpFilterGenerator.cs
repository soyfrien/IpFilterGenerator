using IpFilterLib.Models;

namespace IpFilterLib.Services.Interfaces;
public interface IIpFilterGenerator
{
    List<IpRange> FilterByCountry(List<IpRange> ipRanges, List<string> countryCodes);

    void WriteIpFilteredFile(List<IpRange> ipRanges, string outputPath);
}
