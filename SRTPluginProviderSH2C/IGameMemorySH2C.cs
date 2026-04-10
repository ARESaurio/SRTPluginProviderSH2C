using SRTPluginProviderSH2C.Structs;
using SRTPluginProviderSH2C.Structs.GameStructs;
using System;

namespace SRTPluginProviderSH2C
{
    public interface IGameMemorySH2C
    {
        string GameName    { get; }
        string VersionInfo { get; }

        // Player
        GamePlayer Player  { get; }

        // In-Game Time
        float   IGT               { get; }
        TimeSpan IGTTimeSpan      { get; }
        string  IGTFormattedString { get; }

        // Frame Rate (FPS)
        float FPS { get; }

        // Weapons & Ammo
        short HandgunCount    { get; }
        short HandgunBullets  { get; }
        short ShotgunCount    { get; }
        short ShotgunBullets  { get; }
        short RifleCount      { get; }
        short RifleBullets    { get; }

        // Run Stats
        byte  SaveCount       { get; }
        short ItemCount       { get; }
        short ShootingCount   { get; }
        short FightingCount   { get; }
        float BoatTime        { get; }
        string BoatTimeFormatted { get; }
        float DamageReceived  { get; }

        // Difficulty
        byte   ActionDifficulty       { get; }
        byte   RiddleDifficulty       { get; }
        string ActionDifficultyString { get; }
        string RiddleDifficultyString { get; }

        // Enemies
        NPCInfo[] EnemyHealth { get; }
    }
}
