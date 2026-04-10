using SRTPluginProviderSH2C.Structs;
using SRTPluginProviderSH2C.Structs.GameStructs;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace SRTPluginProviderSH2C
{
    public struct GameMemorySH2C : IGameMemorySH2C
    {
        private const string IGT_TIMESPAN_STRING_FORMAT = @"hh\:mm\:ss";

        public string GameName   => "SH2C";
        public string VersionInfo => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

        // ── Player ───────────────────────────────────────────────────────────
        public GamePlayer Player { get => _player; set => _player = value; }
        internal GamePlayer _player;

        // ── In-Game Time ─────────────────────────────────────────────────────
        public float IGT { get => _igt; }
        internal float _igt;

        // ── Frame Rate ───────────────────────────────────────────────────────
        public float FPS { get => _fps; }
        internal float _fps;

        // ── Weapons & Ammo ───────────────────────────────────────────────────
        public short HandgunCount    { get => _handgunCount; }
        internal short _handgunCount;

        public short HandgunBullets  { get => _handgunBullets; }
        internal short _handgunBullets;

        public short ShotgunCount    { get => _shotgunCount; }
        internal short _shotgunCount;

        public short ShotgunBullets  { get => _shotgunBullets; }
        internal short _shotgunBullets;

        public short RifleCount      { get => _rifleCount; }
        internal short _rifleCount;

        public short RifleBullets    { get => _rifleBullets; }
        internal short _rifleBullets;

        // ── Run Stats ────────────────────────────────────────────────────────
        public byte  SaveCount      { get => _saveCount; }
        internal byte _saveCount;

        public short ItemCount      { get => _itemCount; }
        internal short _itemCount;

        public short ShootingCount  { get => _shootingCount; }
        internal short _shootingCount;

        public short FightingCount  { get => _fightingCount; }
        internal short _fightingCount;

        public float BoatTime       { get => _boatTime; }
        internal float _boatTime;

        public float DamageReceived { get => _damageReceived; }
        internal float _damageReceived;

        // ── Difficulty ───────────────────────────────────────────────────────
        public byte ActionDifficulty { get => _actionDifficulty; }
        internal byte _actionDifficulty;

        public byte RiddleDifficulty { get => _riddleDifficulty; }
        internal byte _riddleDifficulty;

        // ── Enemies ──────────────────────────────────────────────────────────
        public NPCInfo[] EnemyHealth { get => _enemyHealth; }
        internal NPCInfo[] _enemyHealth;

        // ── Calculated Properties ─────────────────────────────────────────────
        public TimeSpan IGTTimeSpan       => TimeSpan.FromSeconds(IGT);
        public string   IGTFormattedString => IGTTimeSpan.ToString(IGT_TIMESPAN_STRING_FORMAT, CultureInfo.InvariantCulture);

        public TimeSpan BoatTimeSpan       => TimeSpan.FromSeconds(BoatTime);
        public string   BoatTimeFormatted  => string.Format("{0:D2}:{1:D2}.{2:D3}", (int)BoatTimeSpan.TotalMinutes, BoatTimeSpan.Seconds, BoatTimeSpan.Milliseconds);

        public string ActionDifficultyString => ActionDifficulty switch
        {
            0 => "Beginner",
            1 => "Easy",
            2 => "Normal",
            3 => "Hard",
            _ => "Unknown"
        };

        public string RiddleDifficultyString => RiddleDifficulty switch
        {
            0 => "Easy",
            1 => "Normal",
            2 => "Hard",
            _ => "Unknown"
        };
    }
}
