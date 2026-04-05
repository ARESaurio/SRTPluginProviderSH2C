using SRTPluginProviderSH2C.Enumerations;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SRTPluginProviderSH2C.Structs
{
    // NOTE: SH2 Classic enemy struct layout.
    // Offsets (ModelType, currentHP) may need adjustment once
    // SH2-specific memory layout is confirmed via Cheat Engine research.
    [DebuggerDisplay("{_DebuggerDisplay,nq}")]
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x158)]
    public struct NPCInfo
    {
        [FieldOffset(0x8)]   private NPCModelTypeEnumeration ModelType;
        [FieldOffset(0x156)] private ushort currentHP;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string _DebuggerDisplay =>
            string.Format("Model: {0} | HP: {1}", ModelType, CurrentHP);

        public NPCModelTypeEnumeration EnemyType      => ModelType;
        public string                  EnemyTypeString => ModelType.ToString();
        public ushort                  CurrentHP       => currentHP;

        // 0xFFFF is typically the "not active / not loaded" sentinel in RE-engine games.
        // May need adjustment for SH2 Classic.
        public bool IsDead  => CurrentHP == 0 || CurrentHP == 0xFFFF;
        public bool IsAlive => !IsDead && CurrentHP > 0;
    }
}
