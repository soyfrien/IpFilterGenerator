using IpFilterLib.Models;
namespace IpFilterLib.Services.Interfaces;
public interface IIpDatabaseLoader
{
    public List<IpRange> LoadFromCsv(string path);

    public List<IpRange> LoadFromStream(Stream stream);
}
