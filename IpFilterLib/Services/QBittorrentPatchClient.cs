using IpFilterLib.Services.Interfaces;

namespace IpFilterLib.Services;
public class QBittorrentPatchClient : IPatchClient
{
    private string _iniPath = string.Empty;
    private readonly string[] _linuxPaths = [
        ".config/qBittorrent/",
        ".var/app/org.qbittorrent.qBittorrent/config/qBittorrent/",
        "snap/qbittorrent-arnatious/current/.config/qBittorrent/" ];

    public string Name => "qBittorrent";


    public bool SettingsExist()
    {
        if(OperatingSystem.IsWindows())
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _iniPath = Path.Combine(appData, $"{Name}", $"{Name}.ini");
        }
        else if (OperatingSystem.IsLinux())
        {
            int configsFound = 0;

            foreach (string path in _linuxPaths)
            {
                _iniPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                                        path,
                                        $"{Name}",
                                        $"{Name}.ini");
                if (File.Exists(_iniPath))
                    configsFound++;
            }

            if (configsFound > 0)
                return true;
        }
        else if (OperatingSystem.IsMacOS())
            _iniPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                                    ".config",
                                    $"{Name}",
                                    $"{Name}.ini");

        return File.Exists(_iniPath); 
    }


    public bool TryPatchSettings(string ipFilterPath)
    {
        if (OperatingSystem.IsLinux())
        {
            // Respect XDG Base Directory Specification (https://specifications.freedesktop.org/basedir-spec/latest/)
            string? xdgConfigHome = Environment.GetEnvironmentVariable("XDG_CONFIG_HOME");
            string? userProfile = !string.IsNullOrEmpty(xdgConfigHome)
                ? xdgConfigHome 
                : Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            foreach (string path in _linuxPaths)
                if (File.Exists(Path.Combine(userProfile, path, $"{Name}", $"{Name}.ini")))
                    try
                    {
                        ApplyPatch(ipFilterPath);
                        BackupSettings(ipFilterPath);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
        else if (OperatingSystem.IsMacOS())
            _iniPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                                    ".config",
                                    $"{Name}",
                                    $"{Name}.ini");
        else if (OperatingSystem.IsWindows())
            _iniPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                    $"{Name}",
                                    $"{Name}.ini");

        if (OperatingSystem.IsMacOS() || OperatingSystem.IsWindows())
            try 
            {
                ApplyPatch(ipFilterPath); 
                BackupSettings(ipFilterPath);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }

        return true;
    }

    private void ApplyPatch(string ipFilterPath)
    {
        List<string>? lines = File.ReadAllLines(_iniPath).ToList();
        int sectionIndex = lines.FindIndex(line => line.Trim() == "[BitTorrent]");
        if (sectionIndex == -1)
            return;

        lines.RemoveAll(line => line.StartsWith(@"IPFilter\"));
        lines.RemoveAll(line => line.StartsWith(@"Session\IPFilter"));
        lines.Insert(sectionIndex + 1, @"Session\IPFilteringEnabled=true");
        lines.Insert(sectionIndex + 2, $"Session\\IPFilter={Path.GetFullPath(ipFilterPath).Replace(@"\", @"\\")}");

        File.WriteAllLines(_iniPath, lines);
    }

    private void BackupSettings(string ipFilterPath) =>
        File.Copy(ipFilterPath, $"{ipFilterPath}-{DateTime.Now}.bak");
}