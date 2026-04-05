using SRTPluginProviderSH2C.Enumerations;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SRTPluginProviderSH2C.Structs.GameStructs
{
    [DebuggerDisplay("{_DebuggerDisplay,nq}")]
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x3)]
    public struct GameItemEntry
    {
        [FieldOffset(0x0)] private byte itemId;
        [FieldOffset(0x1)] private byte stackSize;
        [FieldOffset(0x2)] private byte slotModifier;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string _DebuggerDisplay
        {
            get
            {
                if (IsItem)
                    return string.Format("ID: {0} | Name: {1} | Quantity: {2}", ItemID, ItemID.ToString(), Quantity);
                return "Empty Slot";
            }
        }

        public ItemEnumeration ItemID    => (ItemEnumeration)itemId;
        public string          ItemName  => ItemID.ToString();
        public byte            Quantity  => stackSize;
        public byte            SlotModifier => slotModifier;
        public bool            IsItem    => System.Enum.IsDefined(typeof(ItemEnumeration), itemId);
    }
}