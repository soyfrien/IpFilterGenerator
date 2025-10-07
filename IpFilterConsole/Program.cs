using IpFilterLib.Models;
using IpFilterLib.Services;
using IpFilterLib.Services.Interfaces;
using IpFilterLib.Utils;

#region Process args
string? countriesArg = args.FirstOrDefault((string a) => a.StartsWith("--countries="));
string? loadFromFileArg = args.FirstOrDefault((string a) => a.StartsWith("--load-from-filePath"));
string? outputArg = args.FirstOrDefault((string a) => a.StartsWith("--outputArg="));
string? showLicenseArg = args.FirstOrDefault((string a) => a.StartsWith("--show-license"));
string? patchClientsArg = args.FirstOrDefault((string a) => a.StartsWith("--patch-clients"));
//TODO: patchQbittorrentArg
string? patchQbittorrentArg = args.FirstOrDefault((string a) => a.StartsWith("--patch-qbittorrent"));
const string ip2LocationLicense = @"IPFilterLib uses the IP2Location LITE database for IP geolocation.
© IP2Location.com. All rights reserved.
Users must register at https://lite.ip2location.com to download updates.
IPFilterLib is not associated with IP2Location.com.";
const string about = @"Required flag:
    --countries=[List of two-character, comma-seperated, country codes]
        ipfilterconsole --countries=IL
        ipfilterconsole --countries=IL,AR,PY

Optional flags:
    --load-from-file=[Path to updated CSV file from ip2location.com]
        ipfilterconsole --countries=IL --load-from-file=~/Downloads/IP2LOCATION-LITE-DB1.CSV
    --output=[Path to save generated ipfilter.dat]
    --patch-clients (searches for support BitTorrent clients to apply filter to)
    --patch-qbittorrent (will apply the filter qBittorrent)
    --show-license";

if (args.Length == 0)
{
    Console.WriteLine($"{about}\n");
    Console.WriteLine(ip2LocationLicense);
    return;
}

if (string.IsNullOrEmpty(countriesArg))
    countriesArg = "--countries=IL";

if (!string.IsNullOrEmpty(showLicenseArg))
    Console.WriteLine($"{ip2LocationLicense}\n");

List<IpRange> allRanges = new List<IpRange>();
if (string.IsNullOrEmpty(loadFromFileArg))
{
    using Stream? stream = EmbeddedResourceHelper.GetEmbeddedCsvStream(@"IpFilterLib.Databases.IP2LOCATION-LITE-DB1.CSV");
    allRanges = (new IpDatabaseLoader()).LoadFromStream(stream);
}
else
{
    string filePath = loadFromFileArg.Split('=')[1];
    allRanges = (new IpDatabaseLoader()).LoadFromCsv(filePath);
}

if(!string.IsNullOrEmpty(patchClientsArg) || !string.IsNullOrEmpty(patchQbittorrentArg))
    PatchClients();
#endregion

IpFilterGenerator ipFilterGenerator = new();
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
    Console.WriteLine($"Oops: {ex.Message}");
}


static void PatchClients()
{
    List<IPatchClient> clients = [ new QBittorrentPatchClient() ];

    foreach (IPatchClient client in clients)
    {
        if (client.SettingsExist())
        {
            Console.WriteLine($"Patching {client.Name}...");
            if (client.TryPatchSettings("ipfilter.dat"))
                Console.WriteLine("OK.");
        }
    }
}