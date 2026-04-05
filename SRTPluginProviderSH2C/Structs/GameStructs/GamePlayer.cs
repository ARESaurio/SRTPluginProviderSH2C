using System.Runtime.InteropServices;

namespace SRTPluginProviderSH2C.Structs.GameStructs
{
    // SH2 Classic player data.
    // HP is stored as a float at the base player address.
    // HP thresholds based on SH2 Classic behavior:
    //   Fine   : HP > 600
    //   Caution: HP 300-600
    //   Danger : HP < 300
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 4)]
    public struct GamePlayer
    {
        [FieldOffset(0x0)] private float currentHP;

        public float CurrentHP => currentHP;
        public bool IsAlive     => CurrentHP > 0f;

        public PlayerStatus HealthState =>
            !IsAlive         ? PlayerStatus.Dead    :
            CurrentHP > 75f  ? PlayerStatus.Fine    :
            CurrentHP > 35f  ? PlayerStatus.Caution :
                               PlayerStatus.Danger;

        public string CurrentHealthState => HealthState.ToString();
    }

    public enum PlayerStatus
    {
        Dead,
        Fine,
        Caution,
        Danger,
    }
}
