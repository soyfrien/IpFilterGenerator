| Build Status | VirusTotal Report | Download |
|--------------|-------------------|----------|
| [![Build 🡢 Publish 🡢 Release (linux-arm)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-linux-arm.yml/badge.svg)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-linux-arm.yml) | [Scan Result]() | [Download](https://github.com/soyfrien/IpFilterGenerator/releases/download/v1.2.3.2/IpFilterConsole-linux-arm.zip) |
| [![Build 🡢 Publish 🡢 Release (linux-arm64)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-linux-arm64.yml/badge.svg)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-linux-arm64.yml) | [Scan Result]() | [Download](https://github.com/soyfrien/IpFilterGenerator/releases/download/v1.2.3.2/IpFilterConsole-linux-arm64.zip) |
| [![Build 🡢 Publish 🡢 Release (linux-musl-arm64)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-linux-musl-arm64.yml/badge.svg)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-linux-musl-arm64.yml) | [Scan Result]() | [Download](https://github.com/soyfrien/IpFilterGenerator/releases/download/v1.2.3.2/IpFilterConsole-linux-musl-arm64.zip) |
| [![Build 🡢 Publish 🡢 Release (linux-x64)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-linux.yml/badge.svg)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-linux.yml) | [Scan Result]() | [Download](https://github.com/soyfrien/IpFilterGenerator/releases/download/v1.2.3.2/IpFilterConsole-linux-x64.zip) |
| [![Build 🡢 Publish 🡢 Release (linux-x64)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-linux.yml/badge.svg)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-linux.yml) | [Scan Result]() | [Download](https://github.com/soyfrien/IpFilterGenerator/releases/download/v1.2.3.2/IpFilterConsole-linux-x64.zip) |
| [![Build 🡢 Publish 🡢 Release (macOS)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-mac.yml/badge.svg)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-mac.yml) | | |
| [![Build 🡢 Publish 🡢 Release (Windows)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-windows.yml/badge.svg)](https://github.com/soyfrien/IpFilterGenerator/actions/workflows/build-windows.yml) | [Scan Result]() | [Download](https://github.com/soyfrien/IpFilterGenerator/releases/download/v/IpFilterConsole-win-x64.zip) |


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
 - ```dotnet publish```
