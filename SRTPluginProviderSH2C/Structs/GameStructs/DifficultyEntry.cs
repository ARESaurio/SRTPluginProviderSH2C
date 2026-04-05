using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SRTPluginProviderSH2C.Structs.GameStructs
{
    // SH2 Classic difficulty is tracked as two separate bytes:
    //   ActionDifficulty : 0=Beginner, 1=Easy, 2=Normal, 3=Hard
    //   RiddleDifficulty : 0=Easy,     1=Normal, 2=Hard
    // These are read separately by the scanner and stored in GameMemorySH2C.
    // This struct is kept for reference / future use.
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 2)]
    public struct DifficultyEntry
    {
        [FieldOffset(0x0)] private byte actionDifficulty;
        [FieldOffset(0x1)] private byte riddleDifficulty;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string _DebuggerDisplay =>
            string.Format("Action: {0} | Riddle: {1}", ActionText, RiddleText);

        public byte ActionDifficulty => actionDifficulty;
        public byte RiddleDifficulty => riddleDifficulty;

        public string ActionText => actionDifficulty switch
        {
            0 => "Beginner",
            1 => "Easy",
            2 => "Normal",
            3 => "Hard",
            _ => "Unknown"
        };

        public string RiddleText => riddleDifficulty switch
        {
            0 => "Easy",
            1 => "Normal",
            2 => "Hard",
            _ => "Unknown"
        };
    }
}
