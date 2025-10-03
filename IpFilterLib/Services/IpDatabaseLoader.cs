using IpFilterLib.Models;
using IpFilterLib.Services.Interfaces;

namespace IpFilterLib.Services;

public class IpDatabaseLoader : IIpDatabaseLoader
{
    public List<IpRange> LoadFromCsv(string path)
    {
        List<string> list = (from line in File.ReadAllLines(path).Skip(1)
                             where !string.IsNullOrWhiteSpace(line)
                             select line).ToList();
        List<IpRange> ipRanges = [];

        foreach (string item in list)
        {
            string[] parts = item.Split(',');
            if (parts.Length >= 4)
            {
                IpRange ipRange = new IpRange
                {
                    IpFrom = uint.Parse(parts[0].Trim('"')),
                    IpTo = uint.Parse(parts[1].Trim('"')),
                    CountryCode = parts[2].Trim('"'),
                    CountryName = parts[3].Trim('"')
                };
                ipRanges.Add(ipRange);
            }
        }

        return ipRanges;
    }

    public List<IpRange> LoadFromStream(Stream csvStream)
    {
        using StreamReader? reader = new(csvStream);
        IEnumerable<string>? lines = reader.ReadToEnd()
            .Split('\n')
            .Skip(1)
            .Where(line => !string.IsNullOrWhiteSpace(line));
        List<IpRange>? ipRanges = [];

        foreach (var line in lines)
        {
            string[]? parts = line.Split(',');
            if (parts.Length >= 4)
            {
                ipRanges.Add(new IpRange
                {
                    IpFrom = uint.Parse(parts[0].Trim('"')),
                    IpTo = uint.Parse(parts[1].Trim('"')),
                    CountryCode = parts[2].Trim('"'),
                    CountryName = parts[3].Trim('"')
                });
            }
        }

        return ipRanges;
    }
}
