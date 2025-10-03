using IpFilterLib.Services;

namespace IpFilterLib.Utils;
public class EmbeddedResourceHelper
{
    public static Stream GetEmbeddedCsvStream(string resourceName)
    {
        System.Reflection.Assembly? assembly = typeof(IpDatabaseLoader).Assembly;
        return assembly.GetManifestResourceStream(resourceName)
            ?? throw new FileNotFoundException($"Resource '{resourceName}' not found.");
    }
}
