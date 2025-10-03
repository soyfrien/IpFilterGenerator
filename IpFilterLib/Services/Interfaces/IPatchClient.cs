namespace IpFilterLib.Services.Interfaces;

public interface IPatchClient
{
    public string Name { get; }
    public bool SettingsExist();
    public bool TryPatchSettings(string ipFilterPath);
}
