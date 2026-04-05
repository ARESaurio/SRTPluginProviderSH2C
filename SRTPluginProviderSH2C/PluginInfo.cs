using SRTPluginBase;
using System;

namespace SRTPluginProviderSH2C
{
    internal class PluginInfo : IPluginInfo
    {
        public string Name        => "Game Memory Provider (Silent Hill 2 Classic (2001))";
        public string Description => "A game memory provider plugin for Silent Hill 2 Classic (2001).";
        public string Author      => "Ares & Miguel_mm_95";
        public Uri    MoreInfoURL => new Uri("https://github.com/miguelmm95/Silent_Hill_2_No_Hit_Tool");

        public int VersionMajor    => assemblyVersion.Major;
        public int VersionMinor    => assemblyVersion.Minor;
        public int VersionBuild    => assemblyVersion.Build;
        public int VersionRevision => assemblyVersion.Revision;

        private readonly Version assemblyVersion =
            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
    }
}
