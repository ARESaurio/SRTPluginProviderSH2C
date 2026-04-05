using ProcessMemory;
using SRTPluginProviderSH2C.Structs;
using SRTPluginProviderSH2C.Structs.GameStructs;
using System;
using System.Diagnostics;

namespace SRTPluginProviderSH2C
{
    public unsafe class GameMemorySH2CScanner : IDisposable
    {
        private static readonly uint MAX_ENTITIES = 32U;

        // Memory access handler
        private ProcessMemoryHandler memoryAccess;
        private GameMemorySH2C gameMemoryValues;

        public bool HasScanned;
        public bool ProcessRunning  => memoryAccess != null && memoryAccess.ProcessRunning;
        public uint ProcessExitCode => (memoryAccess != null) ? memoryAccess.ProcessExitCode : 0U;

        // ── Memory Addresses (set per game version in SelectAddresses) ───────
        // All addresses are static offsets from the game's base address.
        // Values obtained from SH2 Classic (PC) memory research and the
        // Silent_Hill_2_No_Hit_Tool by Ares & Miguel_mm_95.
        private ulong AddressPlayerHP;
        private ulong AddressIGT;
        private ulong AddressHandgun;
        private ulong AddressHandgunBullets;
        private ulong AddressShotgun;
        private ulong AddressShotgunBullets;
        private ulong AddressRifle;
        private ulong AddressRifleBullets;
        private ulong AddressSaves;
        private ulong AddressItems;
        private ulong AddressShooting;
        private ulong AddressFighting;
        private ulong AddressBoatTime;
        private ulong AddressDamage;
        private ulong AddressActionDifficulty;
        private ulong AddressRiddleDifficulty;
        private ulong AddressNPCs;

        private nuint BaseAddress { get; set; }

        internal GameMemorySH2CScanner(Process process = null)
        {
            gameMemoryValues = new GameMemorySH2C();
            if (process != null)
                Initialize(process);
        }

        internal void Initialize(Process process)
        {
            if (process == null)
                return;

            // process.MainModule can throw Win32Exception (Access Denied) when the
            // game is launched through DxWnd or with elevated privileges.
            GameVersion version = GameVersion.sh2pc; // Default — only one version supported.
            nuint baseAddr = 0;
            try
            {
                version   = GameHashes.DetectVersion(process.MainModule.FileName);
                baseAddr  = (nuint)process.MainModule.BaseAddress.ToPointer();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // Can't query MainModule — fall back to sh2pc defaults.
                // BaseAddress will be resolved below via ProcessMemoryHandler.
            }

            if (!SelectAddresses(version))
                return; // Unknown version — bail out.

            uint pid = GetProcessId(process).Value;
            memoryAccess = new ProcessMemoryHandler(pid);
            if (ProcessRunning)
                BaseAddress = baseAddr != 0
                    ? baseAddr
                    : (nuint)0x400000; // Standard Win32 load address for SH2 Classic.
        }

        private bool SelectAddresses(GameVersion version)
        {
            switch (version)
            {
                case GameVersion.sh2pc:
                {
                    // ── Player ───────────────────────────────────────────────
                    AddressPlayerHP          = 0x1BB113C;

                    // ── In-Game Time ──────────────────────────────────────────
                    AddressIGT               = 0x19BBF94;

                    // ── Weapons & Ammo ────────────────────────────────────────
                    AddressHandgun           = 0x1B7A7F4;
                    AddressHandgunBullets    = 0x1B7A7F6;
                    AddressShotgun           = 0x1B7A7F8;
                    AddressShotgunBullets    = 0x1B7A7FA;
                    AddressRifle             = 0x1B7A7FC;
                    AddressRifleBullets      = 0x1B7A7FE;

                    // ── Run Stats ─────────────────────────────────────────────
                    AddressSaves             = 0x19BBF8A;
                    AddressItems             = 0x19BBF8E;
                    AddressShooting          = 0x19BBF90;
                    AddressFighting          = 0x19BBF92;
                    AddressBoatTime          = 0x19BBFA0;
                    AddressDamage            = 0x19BBFA8;

                    // ── Difficulty ────────────────────────────────────────────
                    AddressActionDifficulty  = 0x19BBFF4;
                    AddressRiddleDifficulty  = 0x19BBFF5;

                    // ── Enemies ───────────────────────────────────────────────
                    // NOTE: NPC list address needs verification via Cheat Engine.
                    AddressNPCs              = 0x58A114;

                    return true;
                }
            }

            return false; // Unknown version.
        }

        internal IGameMemorySH2C Refresh()
        {
            // ── Player HP ─────────────────────────────────────────────────────
            gameMemoryValues._player = memoryAccess.GetAt<GamePlayer>((void*)(BaseAddress + AddressPlayerHP));

            // ── In-Game Time ──────────────────────────────────────────────────
            gameMemoryValues._igt = memoryAccess.GetAt<float>((void*)(BaseAddress + AddressIGT));

            // ── Weapons & Ammo ────────────────────────────────────────────────
            gameMemoryValues._handgunCount    = memoryAccess.GetAt<short>((void*)(BaseAddress + AddressHandgun));
            gameMemoryValues._handgunBullets  = memoryAccess.GetAt<short>((void*)(BaseAddress + AddressHandgunBullets));
            gameMemoryValues._shotgunCount    = memoryAccess.GetAt<short>((void*)(BaseAddress + AddressShotgun));
            gameMemoryValues._shotgunBullets  = memoryAccess.GetAt<short>((void*)(BaseAddress + AddressShotgunBullets));
            gameMemoryValues._rifleCount      = memoryAccess.GetAt<short>((void*)(BaseAddress + AddressRifle));
            gameMemoryValues._rifleBullets    = memoryAccess.GetAt<short>((void*)(BaseAddress + AddressRifleBullets));

            // ── Run Stats ─────────────────────────────────────────────────────
            gameMemoryValues._saveCount      = memoryAccess.GetByteAt((void*)(BaseAddress + AddressSaves));
            gameMemoryValues._itemCount      = memoryAccess.GetAt<short>((void*)(BaseAddress + AddressItems));
            gameMemoryValues._shootingCount  = memoryAccess.GetAt<short>((void*)(BaseAddress + AddressShooting));
            gameMemoryValues._fightingCount  = memoryAccess.GetAt<short>((void*)(BaseAddress + AddressFighting));
            gameMemoryValues._boatTime       = memoryAccess.GetAt<float>((void*)(BaseAddress + AddressBoatTime));
            gameMemoryValues._damageReceived = memoryAccess.GetAt<float>((void*)(BaseAddress + AddressDamage));

            // ── Difficulty ────────────────────────────────────────────────────
            gameMemoryValues._actionDifficulty = memoryAccess.GetByteAt((void*)(BaseAddress + AddressActionDifficulty));
            gameMemoryValues._riddleDifficulty = memoryAccess.GetByteAt((void*)(BaseAddress + AddressRiddleDifficulty));

            // ── Enemies ───────────────────────────────────────────────────────
            if (gameMemoryValues._enemyHealth == null)
                gameMemoryValues._enemyHealth = new NPCInfo[MAX_ENTITIES];

            for (uint i = 0U; i < MAX_ENTITIES; ++i)
                gameMemoryValues._enemyHealth[i] = memoryAccess.GetAt<NPCInfo>(
                    (void*)(memoryAccess.GetNUIntAt((void*)(BaseAddress + AddressNPCs + (i * 0x4U)))));

            HasScanned = true;
            return gameMemoryValues;
        }

        private uint? GetProcessId(Process process) => (uint?)process?.Id;

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (memoryAccess != null)
                        memoryAccess.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
