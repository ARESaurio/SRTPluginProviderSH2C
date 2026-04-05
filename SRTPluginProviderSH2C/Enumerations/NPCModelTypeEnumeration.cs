namespace SRTPluginProviderSH2C.Enumerations
{
    // NOTE: SH2 Classic enemy model type IDs.
    // These values may need adjustment based on memory research.
    public enum NPCModelTypeEnumeration : byte
    {
        Undefined       = 0,

        // Common enemies
        LyingFigure     = 1,    // Straight-jacket men
        Mannequin       = 2,    // Double-leg creature
        BubbleHeadNurse = 3,    // Bandaged nurses
        Creeper         = 4,    // Small floor bugs
        AirscreamerBird = 5,    // Flying birds

        // Bosses
        PyramidHead     = 10,   // Red Pyramid Thing (boss)
        AbstractDaddy   = 11,   // Boss in hotel
        FleshLip        = 12,   // Boss in hospital
        Mary            = 13,   // Final boss form
        Eddie           = 14,   // Boss fight
    }
}
