using IpFilterLib.Models;
using IpFilterLib.Services;

#region Process args
string? countriesArg = args.FirstOrDefault((string a) => a.StartsWith("--countries="));
string? loadFromFileArg = args.FirstOrDefault((string a) => a.StartsWith("--load-from-filePath"));
string? outputArg = args.FirstOrDefault((string a) => a.StartsWith("--outputArg="));
string? showLicenseArg = args.FirstOrDefault((string a) => a.StartsWith("--show-license"));
const string ip2LocationLicense = @"IPFilterLib uses the IP2Location LITE database for IP geolocation.
© IP2Location.com. All rights reserved.
Users must register at https://lite.ip2location.com to download updates.
IPFilterLib is not associated with IP2Location.com.";
const string about = @"Required flag:
    --countries=[List of two-character, comma-seperated, country codes]
        ipfilterconsole --countries=IL
        ipfilterconsole --countries=IL,AR,PY

Optional flags:
    --load-from-filePath=[Path to updated CSV filePath from ip2location.com]
        ipfilterconsole --countries=IL --load-from-filePath=~/Downloads/IP2LOCATION-LITE-DB1.CSV
    --output=[Path to save generated ipfilter.dat]
    --show-license
";

if (args.Length == 0)
{
    Console.WriteLine(about);
    return;
}

if (string.IsNullOrEmpty(countriesArg))
    countriesArg = "--countries=IL";

if (!string.IsNullOrEmpty(showLicenseArg))
    Console.WriteLine($"{ip2LocationLicense}\n");

List<IpRange> allRanges = new List<IpRange>();
if (string.IsNullOrEmpty(loadFromFileArg))
{
    Stream? stream = (new IpDatabaseLoader()).GetEmbeddedCsvStream(@"IpFilterLib.Databases.IP2LOCATION-LITE-DB1.CSV");
    allRanges = (new IpDatabaseLoader()).LoadFromStream(stream);
}
else
{
    string filePath = loadFromFileArg.Split('=')[1];
    allRanges = (new IpDatabaseLoader()).LoadFromCsv(filePath);
}
#endregion

IpFilterGenerator ipFilterGenerator = new IpFilterGenerator();
List<IpRange> filtered = ipFilterGenerator.FilterByCountry(allRanges, countriesArg.Split('=')[1]
                                                                                                     .Split(',')
                                                                                                     .ToList());
try
{
    ipFilterGenerator.WriteIpFilteredFile(filtered, "ipfilter.dat");
    Console.WriteLine("ipfilter.dat generated successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Oops: {ex}");
}

