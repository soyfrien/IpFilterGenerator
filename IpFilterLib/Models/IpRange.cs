namespace IpFilterLib.Models;
public class IpRange
{
    public required uint IpFrom;

    public required uint IpTo;

    public string CountryCode = "000";

    public string CountryName = "000";
}
