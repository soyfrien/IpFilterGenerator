[![Build and Publish IPFilterGenerator](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build.yml/badge.svg)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build.yml)
[![Build and Publish (Windows)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-windows.yml/badge.svg)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-windows.yml)

# IPFilterConsole
Generates an ipfilter.dat file for chosen countries.

## Usage

```
.\IpFilterConsole
Required flag:
    --countries=[List of two-character, comma-seperated, country codes]
        ipfilterconsole --countries=IL
        ipfilterconsole --countries=IL,AR,PY

Optional flags:
    --load-from-file=[Path to updated CSV file from ip2location.com]
        ipfilterconsole --countries=IL --load-from-file=~/Downloads/IP2LOCATION-LITE-DB1.CSV
    --output=[Path to save generated ipfilter.dat]
    --patch-clients (searches for support BitTorrent clients to apply filter to)
    --patch-qbittorrent (will apply the filter qBittorrent)
    --show-license
```

# IpFilterLib
## Building
 - [Download](https://lite.ip2location.com/ip2location-lite#database) and add ```IP2LOCATION-LITE-DB1.CSV``` to the **Databases** folder.
 - Mark it as an **Embedded resource**.
 - ```dotnet publish```