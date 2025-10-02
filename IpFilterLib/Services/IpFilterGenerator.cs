using IpFilterLib.Models;
using IpFilterLib.Services.Interfaces;
using IpFilterLib.Utils;

namespace IpFilterLib.Services;
public class IpFilterGenerator : IIpFilterGenerator
{
    public List<IpRange> FilterByCountry(List<IpRange> ipRanges, List<string> countryCodes)
    {
        return ipRanges.Where((IpRange ipRange) => countryCodes.Contains(ipRange.CountryCode)).ToList();
    }

    public void WriteIpFilteredFile(List<IpRange> ipRanges, string outputPath)
    {
        using StreamWriter writer = new StreamWriter(outputPath);
        foreach (IpRange range in ipRanges)
        {
            writer.WriteLine($"{IpConverter.DecimalToIPv4(range.IpFrom)} - {IpConverter.DecimalToIPv4(range.IpTo)} , {range.CountryCode} , {range.CountryName}");
        }
    }
}