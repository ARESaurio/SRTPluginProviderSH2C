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

        // SHA256 hash of sh2pc.exe (Silent Hill 2 Enhanced Edition)
        // Hash: F31A221779FADCA4DAE8FE758546ECDE8B88A88FA6E80FAD7DEE115D2EF620E8
        private static readonly byte[] sh2pcEE = new byte[32]
        {
            0xF3, 0x1A, 0x22, 0x17, 0x79, 0xFA, 0xDC, 0xA4,
            0xDA, 0xE8, 0xFE, 0x75, 0x85, 0x46, 0xEC, 0xDE,
            0x8B, 0x88, 0xA8, 0x8F, 0xA6, 0xE8, 0x0F, 0xAD,
            0x7D, 0xEE, 0x11, 0x5D, 0x2E, 0xF6, 0x20, 0xE8
        };


        public static GameVersion DetectVersion(string filePath)
        {
            byte[] checksum;
            using (SHA256 hashFunc = SHA256.Create())
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
                checksum = hashFunc.ComputeHash(fs);

            if (checksum.SequenceEqual(sh2pc))
                return GameVersion.sh2pc;
            if (checksum.SequenceEqual(sh2pcEE))
                return GameVersion.sh2pcEE;
            else
                return GameVersion.Unknown;
        }
    }
}
