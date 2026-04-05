namespace SRTPluginProviderSH2C.Enumerations
{
    // NOTE: Item byte IDs are based on SH2 Classic (PC) memory research.
    // Values may need adjustment if your version differs.
    public enum ItemEnumeration : byte
    {
        None            = 0xFF,

        // Weapons
        Handgun         = 0x00,
        Shotgun         = 0x01,
        HuntingRifle    = 0x02,
        GreatKnife      = 0x03,
        Chainsaw        = 0x04,
        SteelPipe       = 0x08,
        WoodenPlank     = 0x09,

        // Ammo
        HandgunBullets  = 0x05,
        ShotgunShells   = 0x06,
        RifleShells     = 0x07,

        // Health items
        HealthDrink     = 0x0A,
        FirstAidKit     = 0x0B,

        // Puzzle / Key items
        DiaryPage       = 0x0C,
        Flashlight      = 0x0D,
        Radio           = 0x0E,
        MapOfSilentHill = 0x0F,
    }
}
