using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace SRTPluginProviderSH2C
{
    public static class GameHashes
    {
        // SHA256 hash of sh2pc.exe (Silent Hill 2 Classic PC)
        // Hash: 75BF79BA9D364DBA69A2C084B53FE275CD09C8AF5461A8FECE02B64AA64AB7F3
        private static readonly byte[] sh2pc = new byte[32]
        {
            0x75, 0xBF, 0x79, 0xBA, 0x9D, 0x36, 0x4D, 0xBA,
            0x69, 0xA2, 0xC0, 0x84, 0xB5, 0x3F, 0xE2, 0x75,
            0xCD, 0x09, 0xC8, 0xAF, 0x54, 0x61, 0xA8, 0xFE,
            0xCE, 0x02, 0xB6, 0x4A, 0xA6, 0x4A, 0xB7, 0xF3
        };

        public static GameVersion DetectVersion(string filePath)
        {
            byte[] checksum;
            using (SHA256 hashFunc = SHA256.Create())
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
                checksum = hashFunc.ComputeHash(fs);

            if (checksum.SequenceEqual(sh2pc))
                return GameVersion.sh2pc;
            else
                return GameVersion.Unknown;
        }
    }
}
