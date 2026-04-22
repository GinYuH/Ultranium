using System;
using System.Collections.Generic;
using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;
using Ultranium.NPCs.Aldin;
using Ultranium.NPCs.Dread;
using Ultranium.NPCs.Ethereal;
using Ultranium.NPCs.IceDragon;
using Ultranium.NPCs.Ignodium;
using Ultranium.NPCs.Ocean;
using Ultranium.NPCs.ShadowEvent;
using Ultranium.NPCs.ShadowWorm;
using Ultranium.NPCs.TrueDread;
using Ultranium.NPCs.Ultrum;

namespace Ultranium.Achievements
{
    public class SquidAchievement : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<ZephyrSquid>());
        }
    }
    public class IceDragonAchievement : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<IceDragon>());
        }
    }
    public class DreadAchievement : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<DreadBossP2>());
        }
    }
    public class XenanisAchievement : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<Xenanis>());
        }
    }
    public class IgnodiumAchievement : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<Ignodium>());
        }
    }
    public class UltrumAchievement : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<Ultrum>());
        }
    }
    public class TrueDreadAchievement : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<TrueDread>());
        }
    }
    public class MindFlayerAchievement : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<MindFlayer>());
        }
    }
    public class ErebusAchievement : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<ErebusHead>());
        }
    }
    public class ShadowAchievement : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<ErebusHead>());
        }
    }
    public class MushroomAchievement : ModAchievement
    {
        public CustomFlagCondition ShroomCondition { get; private set; }
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Collector);
            ShroomCondition = AddCondition("KeeperMushroom");
        }
    }
    public class AldinAchievement : ModAchievement
    {
        public override bool Hidden => true;
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<Aldin>());
        }
    }
}