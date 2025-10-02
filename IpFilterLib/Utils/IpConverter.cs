namespace IpFilterLib.Utils;
public static class IpConverter
{
    public static string DecimalToIPv4(uint ipDecimalForm) => string.Join('.',
        [
            (ipDecimalForm >> 24) & 0xFF,
            (ipDecimalForm >> 16) & 0xFF,
            (ipDecimalForm >> 8) & 0xFF,
            ipDecimalForm & 0xFF
        ]);

    // TODO: IPv4ToDecimal
    public static string IPv4ToDecimal(string ipv4) => string.Empty;
}
