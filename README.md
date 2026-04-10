# SRTPluginProviderSH2C

![Release](https://img.shields.io/github/v/release/ARESaurio/SRTPluginProviderSH2C?label=current%20release&style=for-the-badge)
![Date](https://img.shields.io/github/release-date/ARESaurio/SRTPluginProviderSH2C?style=for-the-badge)
![Downloads](https://img.shields.io/github/downloads/ARESaurio/SRTPluginProviderSH2C/total?color=%23007EC6&style=for-the-badge)

## Silent Hill 2 Classic (2001) — SRT Memory Provider Plugin

A game memory provider plugin for **SRTHost** that reads in-game data from **Silent Hill 2 Classic (2001)** and **Silent Hill 2 Enhanced Edition**.

### Features

- 🩸 **James HP** — current health value and status (Fine / Caution / Danger / Dead)
- ⏱️ **IGT** — in-game timer (formatted as `hh:mm:ss`)
- 🖥️ **FPS** — current frames per second
- 🔫 **Weapons & Ammo** — Handgun, Shotgun, and Rifle counts and bullet counts
- 📊 **Run Stats** — saves, items collected, enemies defeated by shooting/fighting, total damage received
- 🚤 **Boat Stage Time**
- 🎮 **Difficulty** — Action Level and Riddle Level (shown as name: Beginner / Easy / Normal / Hard)

### Roadmap

Planned features for future versions:

- [x] **Enhanced Edition support** — fully supported, same memory addresses as Classic
- [ ] **Enemy health tracking** — read HP of active enemies in the current room
- [ ] **Born from a Wish support** — memory reading for the extra scenario (Maria's campaign)

### Requirements

- SRTHost 32-bit (x86) v3.1.0.1 or later
- Silent Hill 2 Classic (PC, 2001)
- [DxWnd](https://sourceforge.net/projects/dxwnd/) recommended for compatibility on modern systems
  - When DxWnd asks *"Administrator capability is missing. Do you want to acquire it?"* → click **Cancel**

### Installation

1. Download the latest release
2. Copy the `SRTPluginProviderSH2C` folder into `SRTHost\plugins\`
3. Launch SH2 through DxWnd (click **Cancel** on the admin prompt)
4. Launch `SRTHost32.exe`
5. View data at `http://localhost:7190`

### Building from Source

```
git clone https://github.com/ARESaurio/SRTPluginProviderSH2C.git
git clone https://github.com/SpeedRunTool/SRTHost.git "..\\SpeedRunTool\\SRTHost"
dotnet build SRTPluginProviderSH2C.sln --configuration Debug
```

After building, the compiled `.dll` will be in `SRTPluginProviderSH2C\bin\x86\Debug\net5.0\`. Copy the entire contents of that folder into your SRTHost plugins directory: `SRTHost\plugins\SRTPluginProviderSH2C\`

> **Note:** If your SRTHost is installed at `E:\SRT\`, the plugin copies there automatically after each build.

### Sources & References

This plugin was built on top of the following projects:

- [SRTPluginProviderRE2C](https://github.com/SpeedrunTooling/SRTPluginProviderRE2C) by SpeedrunTooling — base plugin architecture adapted for SH2 Classic
- [Silent Hill 2 No Hit Tool](https://github.com/miguelmm95/Silent_Hill_2_No_Hit_Tool) by Miguel_mm_95 — source of the SH2 Classic memory addresses

### Credits

- Plugin by **Ares** & **Miguel_mm_95**
- Memory addresses based on research from the game using Cheat Engine and the SH2 Classic speedrunning community
- Built on [SRTHost](https://github.com/SpeedRunTool/SRTHost) by Travis J. Gutjahr
