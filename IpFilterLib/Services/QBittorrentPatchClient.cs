using IpFilterLib.Services.Interfaces;

namespace IpFilterLib.Services;
public class QBittorrentPatchClient : IPatchClient
{
    public string Name => "qBittorrent";

    public bool SettingsExist()
    {
        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string iniPath = Path.Combine(appData, "qBittorrent", "qBittorrent.ini");
        return File.Exists(iniPath);
    }

    public bool TryPatchSettings(string ipFilterPath)
    {
        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string iniPath = Path.Combine(appData, Name, $"{Name}.ini");

        if (!File.Exists(iniPath))
            return false;

        List<string>? lines = File.ReadAllLines(iniPath).ToList();
        int sectionIndex = lines.FindIndex(line => line.Trim() == "[BitTorrent]");
        if (sectionIndex == -1)
            return false;

        lines.RemoveAll(line => line.StartsWith("IPFilter\\"));
        lines.Insert(sectionIndex + 1, $"Session\\IPFilteringEnabled=true");
        lines.Insert(sectionIndex + 2, $"Session\\IPFilter={Path.GetFullPath(ipFilterPath).Replace(@"\", @"\\")}");

        File.WriteAllLines(iniPath, lines);
        return true;
    }
}